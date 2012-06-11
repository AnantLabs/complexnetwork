The package contains classes responsible for counting cycles with the specified length in a
standard graph.

A new INeighbourshipContainer interface was created, which currently contains 2 properties:
size and neighbourship. In order to calculate the cycles count in any graph, it must extend this
interface. 

Cycle counting algorithm makes an assumption, that the graph contains vertices indices of which 
start from 0 to Size. Size property must return the number of elements in a graph. 
Neighbourship property returns a dictionary from vertex id to the lists of vertices to which
it is connected.

Also added a new method to calculate all cycles with lengths between the specified range.

The package was tested on BA model. BAContainer class is an example of a graph which can 
implement INeighbourshipContainer interface.