using System;
using System.IO;

namespace template
{
    class Game
    {
	    // member variables
	    public Surface screen;
        Raytracer rayTracer;
	    // initialize
	    public void Init()
	    {
            rayTracer = new Raytracer();
            rayTracer.screen = screen;
	    }
	    // tick: renders one frame
	    public void Tick()
	    {
		    //screen.Clear( 0 );
            rayTracer.Render();
	    }
    }
} // namespace Template