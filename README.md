# AICoreUnity
Small set of scripts for simple movement AI in 2D Unity games. Inspiration taken from the book <em>Artificial Intelligence for Games</em>, which can be found here https://www.amazon.co.uk/Artificial-Intelligence-Games-Ian-Millington/dp/0123747317.

## List of available movement AI
<ul>
<li>Kinematic algorithms: no forces, no accelerations -> constant speed</li>
<ul>
<li>KinematicNone: stay still</li>
<li>KinematicSeek: goes towards the target</li>
<li>KinematicFlee: tries to flee from the target, going in the opposite direction</li>
<li>KinematicArrive: goes towards the target and stops when close</li>
<li>KinematicWander: wander randomly</li>
</ul>
<li>Dynamic algorithms: with forces -> speed not constant</li>
<ul>
<li>DynamicNone: stay still</li>
<li>DynamicSeek: goes towards the target</li>
<li>DynamicFlee: tries to flee from the target, going in the opposite direction</li>
<li>DynamicArrive: goes towards the target and slows down as it gets closer</li>
<li>DynamicAlign: matches the rotation of the target</li>
<li>DynamicVelocityMatch: matches the velocity of the target</li>
</ul>
</ul>

## How to use it
<ol>
<li>Drag the <em>AICore</em>, <em>AICoreUnity</em> and <em>MathNet</em> folders into the same folder in which you keep all your scripts of your project.</li>
<li>Add a <em>RigidBody2D</em> to the object on which you want to add movement AI.</li>
<li>Add the <em>MovementAI</em> script to such object. It can be found in the <em>AICoreUnity</em> directory.</li>
<li>Select some AI parameters and eventually add the target that the object must reach. The target must have a <em>RigidBody2D</em> too.</li>
<li>Enjoy.</li>
</ol>
