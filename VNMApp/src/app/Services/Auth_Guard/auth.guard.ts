import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Auth_Service/auth.service';
import { inject, PLATFORM_ID } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const platformId= inject(PLATFORM_ID);

    if (authService.isAuthenticated()) {
      return true;
    } else {
      router.navigate(['/login']);
      return false;
    }

};
