#include <SDL.h>
#include <SDL_image.h>
#include <iostream>

void exitProgram()
{
	SDL_Quit();

	std::cout << "Press any key to continue..." << std::endl;
	std::cin.get();
}

int main( int argc, char* args[] )
{
	atexit( exitProgram );

	//
	// 1. step: ininitalize SDL
	//

	// initialize the graphical subsystem only
	if ( SDL_Init( SDL_INIT_VIDEO ) == -1 )
	{
		// if it fails, signal error and exit
		std::cout << "[SDL_Init]: " << SDL_GetError() << std::endl;
		return 1;
	}
			
	//
	// 2. step: create a window we can draw to
	//

	SDL_Window *win = 0;
    win = SDL_CreateWindow( "Hello SDL!",				// caption of the window
							100,						// the initial X coordinate of the window
							100,						// the initial Y coordinate of the window
							640,						// width of the window's client area
							480,						// height of the window's client area
							SDL_WINDOW_SHOWN);			// display properties

	// should the window creation fail, notify the user
    if (win == 0)
	{
		std::cout << "[SDL_CreateWindow]: " << SDL_GetError() << std::endl;
        return 1;
    }

	//
	// 3. step: create a renderer
	//

    SDL_Renderer *ren = 0;
    ren = SDL_CreateRenderer(	win, // pointer to the window onto which the renderer should draw
								-1,  // the index of the renderer, if -1: the first renderer fitting our needs specified in the last parameter of this function call
									 // a -1 a harmadik param�terben meghat�rozott ig�nyeinknek megfelel� els� renderel�t jelenti
								SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);	// we want a HW accelerated, vsync-ed renderer
    if (ren == 0)
	{
        std::cout << "[SDL_CreateRenderer]: " << SDL_GetError() << std::endl;
        return 1;
    }

	//
	// 3. step: load the image file
	//
	SDL_Texture* tex = IMG_LoadTexture( ren, "animation_sheet.gif" );
	if ( tex == 0 )
	{
        std::cout << "[IMG_LoadTexture]: " << IMG_GetError() << std::endl;
        return 1;
	}


	//
	// 4. step: start the main loop
	// 

	// do we have to quit?
	bool quit = false;
	// the message of the OS
	SDL_Event ev;
	// the X and Y coordinates of the mouse
	Sint32 mouseX = 0, mouseY = 0;

	int
		pos_x = 0,
		pos_y = 0,
		vel_x = 2,//speed
		vel_y = 2;

	double angle = 0; SDL_RendererFlip flip = SDL_FLIP_NONE;

	while (!quit)
	{
		// process all pending messages in the application message queue
		while (SDL_PollEvent(&ev))
		{
			switch (ev.type)
			{
			case SDL_QUIT:
				quit = true;
				break;
			case SDL_KEYDOWN:
				if (ev.key.keysym.sym == SDLK_ESCAPE)
					quit = true;
				if (ev.key.keysym.sym == SDLK_LEFT) {
					vel_x += 2;
					vel_y += 2;
					vel_x = -vel_x;
					angle = 180;
					flip = SDL_FLIP_VERTICAL;
					break;
				}
				if (ev.key.keysym.sym == SDLK_RIGHT) {
					vel_x -= 3;
					vel_y -= 3;
					vel_x = -vel_x;
					angle = 0;
					flip = SDL_FLIP_NONE;
				}
				break;
			case SDL_MOUSEMOTION:{
				mouseX = ev.motion.x;
				mouseY = ev.motion.y;
				break;
			}
			
			
			}
		}

		// clear the background with white
		SDL_SetRenderDrawColor(ren, 255, 255, 255, 255);
		SDL_RenderClear(ren);


		SDL_Rect image_sized_rect;
		image_sized_rect.x = 0;
		image_sized_rect.y = 0;
		SDL_QueryTexture(tex, 0, 0, &image_sized_rect.w, &image_sized_rect.h);

		SDL_Rect src_rect, dst_rect;

		int frame_index = (SDL_GetTicks() / 100) % 30; //

		src_rect.x = (frame_index % 6) * (image_sized_rect.w / 6);
		src_rect.y = (frame_index / 6) * (image_sized_rect.h / 5);
		src_rect.w = image_sized_rect.w / 6;
		src_rect.h = image_sized_rect.h / 5;
		
		// update position
		pos_x = pos_x + vel_x;
		pos_y = pos_y + vel_y;
		
		if (pos_y > 480 - src_rect.h) {
			vel_y = -vel_y;
		}
		if (pos_x > 640 - src_rect.w) {
			vel_x = -vel_x;
			angle = 180;
			flip = SDL_FLIP_VERTICAL;
		}
		if (pos_y < 0) {
			vel_y = -vel_y;
		}
		if (pos_x < 0) {
			vel_x = -vel_x;
			angle = 0;
			flip = SDL_FLIP_NONE;
		}

		dst_rect.x = pos_x;
		dst_rect.y = pos_y;
		dst_rect.w = src_rect.w;
		dst_rect.h = src_rect.h;

		SDL_RenderCopyEx(ren, tex, &src_rect, &dst_rect, angle, (0,0), flip);


		// draw the image centered at the mouse cursor
		/*SDL_Rect image_rect;
		image_rect.x = mouseX;
		image_rect.y = mouseY;
		image_rect.w = image_sized_rect.w;
		image_rect.h = image_sized_rect.h;

		SDL_RenderCopy( ren,				// which renderer to use
						tex,				// which texture to draw
						&image_sized_rect,	// source rect (which part of the texture to draw)
						&image_rect);		// target rect*/

		// display the contents of the backbuffer
		SDL_RenderPresent(ren);
	}



	//
	// 4. step: clean up
	// 

	SDL_DestroyTexture( tex );
	SDL_DestroyRenderer( ren );
	SDL_DestroyWindow( win );

	return 0;
}