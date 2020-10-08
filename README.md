# Road-network-generation

#The steps of our method:

#(1) road intersection detection 
1) "0preprocessing.py": data preprocessing 
2) KDE was used for data smoothing, which was implemented by ArcGIS
3) "1distance calculation.py": calculating the distance quantity of CFDP algorithm
4) "2road intersection extraction.py": extracting road intersections by decision graph
5) "3filtration.py": Removeing pseudo intersections that fall outside the road

#(2) Link identification

This main functions, which were implemented by C# based on the platform of Arcengine, are listed as follows: 
1) deleting peripheral long and narrow triangles
2) Criterion 1-6
3) sub-trajectory point extraction
4) Optimizing by morphological methods

#(3) Link fitting
