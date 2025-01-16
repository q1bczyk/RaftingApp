import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from "../../auth/services/auth.service";
import { isPlatformBrowser } from "@angular/common";

@Injectable({
    providedIn: 'root'
})

export class AdminGuard implements CanActivate{
    constructor(
        private authService: AuthService,
        private router: Router,
        @Inject(PLATFORM_ID) private platformId: Object
      ) {}
    
      canActivate(): boolean {
        if (isPlatformBrowser(this.platformId)) {
          if (this.authService.isAdmin())
            return true; 
          else {
            this.router.navigate(['/not-found']);
            return false; 
          }
        }
        return false; 
      }
}