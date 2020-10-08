# Road-network-generation

#The steps of our method:
#(1) road intersection detection 
1) "0preprocessing.py": data preprocessing 
2) KDE was used for data smoothing, which was implemented by ArcGIS
3) "1distance calculation.py": calculating the distance quantity of CFDP algorithm
4) "2road intersection extraction.py": extracting road intersections by decision graph
5) "3filtration.py": Removeing pseudo intersections that fall outside the road
#(2) Link identification

This function was implemented by C# based on the platform of Arcengine 

#(3) Link fitting
