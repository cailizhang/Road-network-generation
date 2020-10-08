import matplotlib.pyplot as plt
import numpy as np
import math
import os
from scipy import signal
import sys

def corepoints(data, p1, p2):
    result=[]
    
    #求密度
    rho,maxd=dens(data,20)
    #求距离
    dis=SurfDist(p1,p2)
    n=int(dis/15)
    # 一般式直线方程系数 A*x+B*y+C=0
    
    A = (p1[1] - p2[1]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    B = (p2[0] - p1[0]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    C = (p1[0] * p2[1] - p2[0] * p1[1]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    #平分20
    points=[]
    p11=[p1[0],p1[1]]
    p22=[p2[0],p2[1]]
    points.append(p11)
    
    for i in range(1,n):
       
        point=[(p1[0]+(p2[0]-p1[0])*i/n),(p1[1]+(p2[1]-p1[1])*i/n)]
        points.append(point)
    points.append(p22)
    
    #求垂线
    for m in range(0,n-2):
        A1=0
        A2=0
        B1=0
        B2=0
        C1=0
        C2=0
        
        if A==0:
            x0=points[m+1][0]
            A1=1
            B1=0
            C1=-x0
            
            x00=points[m+2][0]
            A2=1
            B2=0
            C2=-x00
        elif B==0:
            y0=points[m+1][1]
            A1=0
            B1=1
            C1=-y0
            
            y00=points[m+2][1]
            A2=0
            B2=1
            C2=-y00
            
        else:
            x0=points[m+1][0]
            y0=points[m+1][1]            
            A1=1/A
            B1=-1/B
            C1=y0/B-x0/A
            
            x00=points[m+2][0]
            y00=points[m+2][1]            
            A2=1/A
            B2=-1/B
            C2=y00/B-x00/A
            
        candidate=[]
        rho1=[]
        for i in range(len(data)):
            d1 = abs(A1 * data[i][0] + B1 * data[i][1] + C1) / math.sqrt(math.pow(A1, 2) + math.pow(B1, 2))
            d2 = abs(A2 * data[i][0] + B2 * data[i][1] + C2) / math.sqrt(math.pow(A2, 2) + math.pow(B2, 2))
            if d1+d2<=toDegree(dis/n):
                candidate.append(data[i])
                rho1.append(rho[i])
        lab=0
        Max=0
        if len(candidate)>0:
            
            for m in range(len(rho1)):
                if rho1[m]>Max:
                    Max=rho1[m]
                    lab=m
            result.append(candidate[lab])
        
    
    return result    

def SurfDist(pt1,pt2):
    yd=0.5*(pt1[1]-pt2[1])*math.pi/180
    xd=0.5*(pt1[0]-pt2[0])*math.pi/180
    yd=math.pow(yd,2)
    xd=math.pow(xd,2)
    f1=math.sin(xd)
    f2=math.sin(yd)
    f3=math.cos(pt1[1]*math.pi/180.0)*math.cos(pt2[1]*math.pi/180.0)
    dist=2.0*6378137.0*math.asin(math.sqrt(f1+f2*f3))
    return dist
def dens(data,bandwidth):
    maxd=0
    rho_avg=0
    rho=np.zeros(shape=len(data))
    for i in range(len(data)):
        for j in range(len(data)):
            if(j!=i):
            
               dist=SurfDist(data[i],data[j])
               if dist> maxd:
                   
                  maxd=dist
                   
               elif dist< bandwidth:
                    
                    rho[i]=rho[i]+1
            
    return rho,maxd

#反DP
result = []
def compress(copydata, p1, p2):





    # 一般式直线方程系数 A*x+B*y+C=0
    data=[]
    rho,maxd=dens(copydata,20)
    for i in range(len(rho)):
        if (rho[i]>1):
            data.append(copydata[i])

    A = (p1[1] - p2[1]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    B = (p2[0] - p1[0]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    C = (p1[0] * p2[1] - p2[0] * p1[1]) / math.sqrt(math.pow(p1[1] - p2[1], 2) + math.pow(p1[0] - p2[0], 2))
    distance = []
    for i in range(len(data)):
        d = abs(A * data[i][0] + B * data[i][1] + C) / math.sqrt(math.pow(A, 2) + math.pow(B, 2))
        distance.append(d)
    if (len(distance) == 0):
        dmax = 0
    else:
        dmax = max(distance)

    
    if dmax > toDegree(20):

        for i in range(len(data)):
            if (abs(A * data[i][0] + B * data[i][1] + C) / math.sqrt(math.pow(A, 2) + math.pow(B, 2)) == dmax):
                middle = data[i]

        #垂足？
        m=middle[0]
        n=middle[1]

       
        if(A*A+B*B<1e-13):
            center=[p1[0],p1[1]]
        elif(np.abs (A*m+B*n+C)<1e-13):
            center=[m,n]
        else:
            center = [(B*B*m-A*B*n-A*C)/(A*A+B*B),(A*A*n-A*B*m-B*C)/(A*A+B*B)]

        
        

        # 左向量、右向量
        data1 = []
        data2 = []
        for i in range(len(data)):
            difx = data[i][0] - middle[0]
            dify = data[i][1] - middle[1]
            diffx = center[0] - middle[0] 
            diffy = center[1] - middle[1] 
            if ((difx * diffy - diffx * dify)*d > 0):
                data1.append(data[i])
            else:
                data2.append(data[i])

        compress(data1, p1, middle)
        result.append(middle)

        compress(data2, middle, p2)

    return result
    
    


def Exist(file,lab):
    result=False
    count=0
    for i in range(len(lab)):
        if file==lab[i]:
            count=count+1
    if count>0:
        result=True
    return result


def Exist1(point,result):
    a=False
    count=0
    for i in range(len(result)):
        if (point[0]==result[i][0])&(point[1]==result[i][1]):
            count=count+1
    if count>0:
        a=True
    return a
 #排序



def Xsort(result1,result2):
    result=[]
    for i in range(len(result1)):
        point=result1[i]
        if (Exist1(point,result2)==True)&(Exist1(point,result)==False):
            result.append(point)
        

    return result
           









    
ER = 6378137.
refPt =[30.5,114.3]
def toDegree(nMeters):
    det = 0.0001
    otherPt = [refPt[0]+det, refPt[1]]
    dist = SurfDist(refPt, otherPt)
    degPerMet = det/dist
    return nMeters*degPerMet

path1=r'.\data\交叉点过滤10.txt'
data1 = np.loadtxt(path1, delimiter=',', usecols=[0, 1])

path2 =r'.\data\strip'
dirs=os.listdir(path2)

path3=r'.\data\Sline.txt'
f=open(path3,"w")
count=0
lab=[]

for file in dirs:
    result=[]
    if Exist(file,lab):
        continue

    
    root=r'%s\%s'%(path2,file)
    path,name=os.path.split(root)
    a=os.path.splitext(name)[0]
    Spoint=a.split('-')[0]
    Epoint=a.split('-')[1]
    count1=0
    
    for file1 in dirs:
        root1=r'%s\%s'%(path2,file1)
        path1,name1=os.path.split(root1)
        a=os.path.splitext(name1)[0]
        Spoint1=a.split('-')[0]
        Epoint1=a.split('-')[1]
        if (Epoint==Spoint1)&(Spoint==Epoint1):
            lab.append(file1)
            
            count1=count1+1
    if count1==1:
        
        root2=r'%s\%s'%(path2,Epoint+'-'+Spoint+'.txt')
        data2=np.loadtxt(root, delimiter=',', usecols=[1, 0])
        data3=np.loadtxt(root2, delimiter=',', usecols=[1, 0])
        data=np.vstack((data2,data3))
        p0=data1[int(Spoint)]
        pn=data1[int(Epoint)]
        copydata=corepoints(data, p0, pn)
       
        
        center = [(p0[0] + pn[0]) / 2, (p0[1] + pn[1]) / 2]
        
        result2=compress(copydata,data1[int(Spoint)],data1[int(Epoint)])
        
        result3=Xsort(copydata,result2)
        
        f.write(str(count))
        f.write(',')
        f.write(str(Spoint))
        f.write(',')
        f.write(str(Epoint))
        f.write(',')
        f.write(str(data1[int(Spoint)][0]))
        f.write(',')
        f.write(str(data1[int(Spoint)][1]))
        f.write(',')
        f.write('双')
        f.write("\n")
        
        for k in range(len(result3)):
            f.write(str(count))
            f.write(',')
            f.write(str(Spoint))
            f.write(',')
            f.write(str(Epoint))
            f.write(',')
            f.write(str(result3[k][0]))
            f.write(',')
            f.write(str(result3[k][1]))
            f.write(',')
            f.write('双')
            f.write("\n")
        f.write(str(count))
        f.write(',')
        f.write(str(Spoint))
        f.write(',')
        f.write(str(Epoint))
        f.write(',')
        f.write(str(data1[int(Epoint)][0]))
        f.write(',')
        f.write(str(data1[int(Epoint)][1]))
        f.write(',')
        f.write('双')
        f.write("\n")   
       
    else:

        data=np.loadtxt(root, delimiter=',', usecols=[1,0])
        copydata=[]
        
        p0=data1[int(Spoint)]
        pn=data1[int(Epoint)]
        copydata=corepoints(data, p0, pn)
        center = [(p0[0] + pn[0]) / 2, (p0[1] + pn[1]) / 2]

        result2=compress(copydata,data1[int(Spoint)],data1[int(Epoint)])
        
        result3=Xsort(copydata,result2)     

        


        f.write(str(count))
        f.write(',')
        f.write(str(Spoint))
        f.write(',')
        f.write(str(Epoint))
        f.write(',')
        f.write(str(data1[int(Spoint)][0]))
        f.write(',')
        f.write(str(data1[int(Spoint)][1]))
        f.write(',')
        f.write('单')
        f.write("\n")
        for k in range(len(result3)):
            f.write(str(count))
            f.write(',')
            f.write(str(Spoint))
            f.write(',')
            f.write(str(Epoint))
            f.write(',')
            f.write(str(result3[k][0]))
            f.write(',')
            f.write(str(result3[k][1]))
            f.write(',')
            f.write('单')
            f.write("\n")
        f.write(str(count))
        f.write(',')
        f.write(str(Spoint))
        f.write(',')
        f.write(str(Epoint))
        f.write(',')
        f.write(str(data1[int(Epoint)][0]))
        f.write(',')
        f.write(str(data1[int(Epoint)][1]))
        f.write(',')
        f.write('单')
        f.write("\n")
    count=count+1
           


f.close()


    


