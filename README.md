
Lagrange polynomial inverse interpolation.


This algorithm uses two loops. Each of these ones  runs through all n values of the input array. So complexity of algorythm is $O(n^2)$ .
But there is another loop in program that runs through values of function. This one takes m steps. So full complexity is equal to  $O(m*n^2)$
