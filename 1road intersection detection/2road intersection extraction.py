
import matplotlib.pyplot as plt
import numpy as np
import math
import os




root = r'.\xuandian.txt'


root2 = r'.\距离.txt'
data1 = np.loadtxt(root, delimiter=',', usecols=[0,1, 2])

rho=data1[:,0]
data=data1[:,1:3]

delta=np.loadtxt(root2, delimiter=',', usecols=[0, 1])



#画detal图  及交叉口，并输出txt文件      
def extract(rho,delta,data):
    
    f=open(r'.\交叉点25.txt',"w")


   

    plt.figure(num=1, figsize=(15, 9))
    for i in range(len(rho)):
        plt.scatter(x=rho[i], y=delta[i][1], c='k', marker='o', s=15)
        
    plt.xlabel('density')
    plt.ylabel('distance')
    plt.title('Chose Leader')
    plt.show()

    plt.figure(num=2,figsize=(15,9))
    for j in range(len(data)):
        plt.scatter(x=data[j, 0], y=data[j, 1], marker='o', c='k', s=8)
        #if(rho[j]>rho_avg)&(delta[j][1]>(bandwidth)):
        if(delta[j][1]>25):
            
            #plt.annotate(j, xy=(data[j][0], data[j][1]), xytext=(data[j][0]+0.001, data[j][1]+0.001),arrowprops=dict(facecolor='black', shrink=0.05))

            plt.scatter(x=data[j][0], y=data[j][1], marker='+', s=200, c='b')
            f.write(str(data[j][0]))
            f.write(',')
            f.write(str(data[j][1]))
            f.write("\n")
    f.close() 
    plt.xlabel('axis_x')
    plt.ylabel('axis_y')
    plt.title('Dataset')
    plt.show()


extract(rho,delta,data)






