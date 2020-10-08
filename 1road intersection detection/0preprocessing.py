from collections import defaultdict
from scipy.spatial import cKDTree
import numpy as np
import time
import operator
import geopy
import geopy.distance
import math as mt
import pandas as pd
import os
import matplotlib.pyplot as plt
ER=6378137.
def SurDist(Pt1, Pt2):
    
    tmp00 = 0.5*(Pt1[3]-Pt2[3])*mt.pi/180.
    tmp01 = 0.5*(Pt1[2]-Pt2[2])*mt.pi/180.
    tmp1 = mt.pow(mt.sin(tmp00), 2.)
    tmp2 = mt.pow(mt.sin(tmp01), 2.)
    tmp3 = mt.cos(Pt1[3]*mt.pi/180.) * mt.cos(Pt2[3]*mt.pi/180.)
    dist = 2.0*ER*mt.asin(mt.sqrt(tmp1 + tmp2*tmp3))
    
    return dist

def SurfDist(Pt1, Pt2):
    
    tmp00 = 0.5*(Pt1[2]-Pt2[2])*mt.pi/180.
    tmp01 = 0.5*(Pt1[3]-Pt2[3])*mt.pi/180.
    tmp1 = mt.pow(mt.sin(tmp00), 2.)
    tmp2 = mt.pow(mt.sin(tmp01), 2.)
    tmp3 = mt.cos(Pt1[2]*mt.pi/180.) * mt.cos(Pt2[2]*mt.pi/180.)
    dist = 2.0*ER*mt.asin(mt.sqrt(tmp1 + tmp2*tmp3))
    
    return dist
#求交叉点
def intersection(point1, point2):
    if point1[5]==90:
        if point2[5]==0:
           turningpointx=point1[2]
           turningpointy=point2[3]
        else:
           k2=mt.tan(point2[5]*mt.pi/180)
           b2=point2[3]-k2*point2[2]
           turningpointx=point1[2]
           turningpointy=point1[2]*k2+b2
    else:
        k1=mt.tan(point1[5]*mt.pi/180)
        b1=point1[3]-point1[2]*k1
        k2=mt.tan(point2[5]*mt.pi/180)
        b2=point2[3]-k2*point2[2]
        turningpointx=(b2-b1)/(k1-k2)
        turningpointy=k1*turningpointx+b1
    return turningpointx,turningpointy


def load_data(fname):
    data_points = list()
    f=open(fname, 'r') 
    for line in f:
            
	     vehicule_id, timestamp, Y, X, speed, angle = line.split(',')
	     pt=np.zeros(shape=(1,6))
	     pt[0][0]=vehicule_id
	     pt[0][1]=time.mktime(time.strptime(timestamp, "%Y-%m-%d %H:%M:%S"))
	     pt[0][2]=Y
	     pt[0][3]=X
	     pt[0][4]=speed
	     pt[0][5]=angle
	     
	     data_points.append(pt)
    data_points=np.array(data_points)
    
    return np.squeeze(data_points )

def diffangles(a1, a2):
	"""
	Difference between two angles in 0-360 degrees.
	:param a1: angle 1
	:param a2: angle 2
	:return: difference
	180 - abs(abs(a1 - a2) - 180)
	"""
	return 180 - abs(abs(a1 - a2) - 180)


def deletcopoint(data):
    f=open(r'.\pretreatment1.txt',"w")
    f.write(str(data[0][0]))
    f.write(',')
    f.write(str(data[0][1]))
    f.write(',')
    f.write(str(data[0][2]))
    f.write(',')
    f.write(str(data[0][3]))
    f.write(',')
    f.write(str(data[0][4]))
    f.write(',')
    f.write(str(data[0][5]))

    f.write("\n")
    for i in range(1,len(data)):
    
         
        if (SurDist(data[i-1],data[i])<5):
            continue
        else:
            f.write(str(data[i][0]))
            f.write(',')
            f.write(str(data[i][1]))
            f.write(',')
            f.write(str(data[i][2]))
            f.write(',')
            f.write(str(data[i][3]))
            f.write(',')
            f.write(str(data[i][4]))
            f.write(',')
            f.write(str(data[i][5]))
       
            f.write("\n")
    f.close()  
        
root = r'.\7天.txt'

data=load_data(root)

f=open(r'.\pretreatment0.txt',"w")

for i in range(len(data)):
    if (data[i][5]==0)|(data[i][4]>1000/36)|(data[i][4]==0):
        continue
    else:
              
        f.write(str(data[i][0]))
        f.write(',')
        f.write(str(data[i][1]))
        f.write(',')
        f.write(str(data[i][2]))
        f.write(',')
        f.write(str(data[i][3]))
        f.write(',')
        f.write(str(data[i][4]))
        f.write(',')
        f.write(str(data[i][5]))
           
        f.write("\n")
f.close()

root1 = r'.\pretreatment0.txt'

data= np.loadtxt(root1, delimiter=',', usecols=[0, 1,2,3,4,5])


while True:
    m=len(data)
    
    deletcopoint(data)
    path=r'.\pretreatment1.txt'
    data=np.loadtxt(path, delimiter=',', usecols=[0, 1,2,3,4,5])
    n=len(data)
   
    if m==n:
        break

root2 = r'.\pretreatment1.txt'

data_points= np.loadtxt(root2, delimiter=',', usecols=[0, 1,2,3,4,5])


f=open(r'.\TurningAngle.txt',"w")

f.write(str(int(data_points[0][0])))
f.write(',')
f.write(str(data_points[0][1]))
f.write(',')
f.write(str(data_points[0][3]))
f.write(',')
f.write(str(data_points[0][2]))
f.write(',')
f.write(str(data_points[0][4]))
f.write(',')
f.write(str(data_points[0][5]))
f.write(',')
f.write(str(0))  
f.write("\n")

for i in range(1,len(data_points)):
    a1=data_points[i][1]
    
    a2=data_points[i-1][1]

    if (data_points[i][0] ==data_points[i-1][0]):     
       
       #&(a1-a2<300)
       
       f.write(str(int(data_points[i][0])))
       f.write(',')
       f.write(str(data_points[i][1]))
       f.write(',')
       f.write(str(data_points[i][3]))
       f.write(',')
       f.write(str(data_points[i][2]))
       f.write(',')
       f.write(str(data_points[i][4]))
       f.write(',')
       f.write(str(data_points[i][5]))
       f.write(',')
       f.write(str(diffangles(data_points[i][5],data_points[i-1][5])))  
       f.write("\n")


       
    else:
        f.write(str(int(data_points[i][0])))
        f.write(',')
        f.write(str(data_points[i][1]))
        f.write(',')
        f.write(str(data_points[i][3]))
        f.write(',')
        f.write(str(data_points[i][2]))
        f.write(',')
        f.write(str(data_points[i][4]))
        f.write(',')
        f.write(str(data_points[i][5]))
        f.write(',')
        f.write(str(0))  
        f.write("\n")

f.close()         

root3 = r'.\TurningAngle.txt'

data= np.loadtxt(root3, delimiter=',', usecols=[0, 1,2,3,4,5,6])
f=open(r'.\TurningPointintersection.txt',"w")
plt.figure(num=1, figsize=(15, 9))
for i in range(1,len(data)):
    
    

    
    #if (data[i][6]>60)&(data[i][6]<120)&(abs(data[i][1]-data[i-1][1])<300)&(abs(data[i][1]-data[i-1][1])>20)&(SurfDist(pt1,pt2)<300):
    if (data[i][6]>45)&(data[i][6]<135)&(SurfDist(data[i],data[i-1])<200):
        
        turningpointx,turningpointy=intersection(data[i],data[i-1])
        plt.scatter(x=turningpointy,y=turningpointx, c='k', marker='o', s=15)
        f.write(str(turningpointx))
        f.write(',')
        f.write(str(turningpointy))


        f.write("\n")
f.close()
plt.xlabel('X-cordinate')
plt.ylabel('Y-cordinate')
plt.title('Representative intersections')
plt.show()
