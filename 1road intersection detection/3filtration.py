from collections import defaultdict
from scipy.spatial import cKDTree
import numpy as np
import time
import operator
import geopy
import geopy.distance
import math
import pandas as pd
import os
import math as mt
ER = 6378137.
BandLen = 85.

def SurDist(Pt1, Pt2):
    
    tmp00 = 0.5*(Pt1[0]-Pt2[0])*mt.pi/180.
    tmp01 = 0.5*(Pt1[1]-Pt2[1])*mt.pi/180.
    tmp1 = mt.pow(mt.sin(tmp00), 2.)
    tmp2 = mt.pow(mt.sin(tmp01), 2.)
    tmp3 = mt.cos(Pt1[0]*mt.pi/180.) * mt.cos(Pt2[0]*mt.pi/180.)
    dist = 2.0*ER*mt.asin(mt.sqrt(tmp1 + tmp2*tmp3))
    
    return dist



def Within(Pt, ptCen, radius):
    isIn = False
    if SurDist(Pt, ptCen) < radius:
        isIn = True
    
    return isIn


intDir=r".\交叉点25.txt"
Intersection=np.loadtxt(intDir,delimiter=',',usecols=[1,0])
path=r".\交叉点过滤10.txt"

f=open(path,'w')

root = r'.\pretreatment1.txt'
data=np.loadtxt(root,delimiter=',',usecols=[3,2])


for j in range(0,len(Intersection)):
    intersection=[Intersection[j][0],Intersection[j][1]]
    count=0


    for i in range(0,len(data)):
        p=[data[i][0],data[i][1]]
        

        if Within(p,intersection,20):
            count=count+1

    if count>10:
        f.write(str(Intersection[j][0]))
        f.write(',')
        f.write(str(Intersection[j][1]))
        
        f.write('\n')
            
f.close()    
print("Finished!\n")











