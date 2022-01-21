# Orbits-and-Trajectories

Orbits and Trajectories is a small Windows Presentation Foundation app designed to simulate the orbital dynamics of a child body around a parent body.

A parent body is initiated and so is a child. The child has x and y position, and x and y velocity. 

The gravitational force vector is calculated through inverse square law (the closer the child body is to the parent body the stronger the gravity).

Inverse tan of the gradient of the line between child and parent bodies are used to calculate the angle between them. This angle is used to resolve the diagonal gravitational force vector into x and y acceleration vectors.

These x and y accelerations are then applied to the child body's x and y velocities, and its position is updated.
