import { isPlatformBrowser } from "@angular/common";
import { Injectable, PLATFORM_ID, WritableSignal, inject, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class NavigationService{
    private platformId = inject(PLATFORM_ID);
    private resizeObserver: (() => void) | null = null;
    private menuState : WritableSignal<boolean> = signal(true);
    private screenWidth: WritableSignal<number> = signal(0); 

    constructor() {
        if (isPlatformBrowser(this.platformId)) this.resizeObserver = this.createResizeObserver();
        
      }

    openMenu(){
        this.menuState.set(true);
    }

    closeMenu(){
        this.menuState.set(false);
    }

    isMenuOpen() : boolean{
        return this.menuState();
    }

    getScreenWidth() : number{
        return this.screenWidth();
    }

    private createResizeObserver() {
        const onResize = () => {
          const width = window.innerWidth;
          this.screenWidth.set(width);
          if (width < 1400) 
            this.closeMenu(); 
            else this.openMenu();
        };
        window.addEventListener('resize', onResize);
        onResize();
        return () => window.removeEventListener('resize', onResize);
      }
    
      ngOnDestroy() {
        if (this.resizeObserver) this.resizeObserver();
      }
}