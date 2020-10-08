import matplotlib.pyplot as plt
import numpy as np
import math
import os
from scipy import signal


'''
def SurfDist(pt1,pt2):
    yd=0.5*(pt1[1]-pt2[1])*math.pi/180
    xd=0.5*(pt1[0]-pt2[0])*math.pi/180
    yd=math.pow(yd,2)
    xd=math.pow(xd,2)
    f1=math.sin(yd)
    f2=math.sin(xd)
    f3=math.cos(pt1[0]*math.pi/180.0)*math.cos(pt2[0]*math.pi/180.0)
    dist=2.0*6378137.0*math.asin(math.sqrt(f1+f2*f3))
    return dist
'''
def SurfDist(pt1,pt2):
    xd=0.5*(pt1[1]-pt2[1])*math.pi/180
    yd=0.5*(pt1[0]-pt2[0])*math.pi/180
    xd=math.pow(xd,2)
    yd=math.pow(yd,2)
    
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
                    rho[i]=np.sum(np.exp(-(dist/bandwidth) ** 2))                     
        rho_avg=rho_avg+rho[i]
    rho_avg=rho_avg/len(data)    
    return rho,rho_avg,maxd

def delta(rho,maxd,data):
    delta=np.zeros(shape=(len(data),2))
    rho_sorted=sorted(enumerate(rho),key=lambda x:x[1],reverse = True)
    delta[rho_sorted[0][0]][1]=maxd
    delta[rho_sorted[0][0]][0]=0
    for i in range(1,len(rho)):
        delta[rho_sorted[i][0]][1]=maxd+1.0
        for j in range(i):
            dist=SurfDist(data[rho_sorted[i][0]],data[rho_sorted[j][0]])
            if dist<delta[rho_sorted[i][0]][1]:
               delta[rho_sorted[i][0]][1]=dist
               delta[rho_sorted[i][0]][0]=rho_sorted[j][0]
    return delta


root = r'.\xuandian.txt'

path2 = r'.\距离.txt'

bandwidth=75
data1 = np.loadtxt(root, delimiter=',', usecols=[0, 1,2])
rho=data1[:,0]
data=data1[:,1:3]

rho_avg=np.mean(rho)
maxd=0
for i in range(len(data)):
    for j in range(len(data)):
        if(j!=i):
            dist=SurfDist(data[i],data[j])
            if dist> maxd:
                maxd=dist




delta1=delta(rho,maxd,data)



f=open(path2,"w")
for i in range(len(delta1)):
    f.write(str(int(delta1[i][0])))
    f.write(',')
    f.write(str(delta1[i][1]))
    f.write("\n")
f.close()    
    


