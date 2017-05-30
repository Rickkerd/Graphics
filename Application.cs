using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;


namespace template
{
    class Application
    {
        public float moveX;
        public float moveY;
        public float moveZ;
        public Vector3 move;

        public Vector3 Control()//Handles the movement of the camera
        {
            var keyboard = Keyboard.GetState();
            if (keyboard[Key.A]) moveX += -0.1f;//Move to the left
            if (keyboard[Key.D]) moveX += 0.1f;//Move to the right
            if (keyboard[Key.S]) moveZ += -0.1f;//Move forward
            if (keyboard[Key.W]) moveZ += 0.1f;//Move backwards
            if (keyboard[Key.F]) moveY += -0.1f;//Move up
            if (keyboard[Key.R]) moveY += 0.1f;//Move down

            move = new Vector3(moveX, moveY, moveZ);//Move vector
            return move;
        }
    }
}
