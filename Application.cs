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

        public Vector3 Control()
        {
            var keyboard = Keyboard.GetState();
            if (keyboard[Key.A]) moveX = -1;
            if (keyboard[Key.D]) moveX = 1;
            if (keyboard[Key.S]) moveZ = -1;
            if (keyboard[Key.W]) moveZ = 1;
            if (keyboard[Key.F]) moveY = -1;
            if (keyboard[Key.R]) moveY = 1;

            move = new Vector3(moveX, moveY, moveZ);
            return move;
        }
    }
}
