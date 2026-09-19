import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next:HttpHandlerFn):Observable<HttpEvent<any>> => {
  
    const token = localStorage.getItem('access_token'); // Or use a service to fetch it
    const isFormData = req.body instanceof FormData;

    if (token) {
      const headers: Record<string, string> = {
      Authorization: `Bearer ${token}`
      };

      if (!isFormData) {
        headers['Content-Type'] = 'application/json';
      }

      const cloned = req.clone({
        setHeaders: headers
      });
      return  next(cloned).pipe(
      catchError((error: HttpErrorResponse) => {
        // 🔍 Centralized error handling logic
        if (error.status === 401) {
          console.warn('Unauthorized - maybe redirect to login');
        } else if (error.status === 403) {
          console.warn('Forbidden - show access denied message');
        } else if (error.status === 500) {
          console.error('Server error - show generic error message');
        } else {
          console.error(`Unhandled error: ${error.message}`);
        }
        // Optionally rethrow the error to let components handle it too
        return throwError(() => error);
      }));
      
    }

    return next(req).pipe(
      catchError((error: HttpErrorResponse) => {
        // 🔍 Centralized error handling logic
        if (error.status === 401) {
          console.warn('Unauthorized - maybe redirect to login');
        } else if (error.status === 403) {
          console.warn('Forbidden - show access denied message');
        } else if (error.status === 500) {
          console.error('Server error - show generic error message');
        } else {
          console.error(`Unhandled error: ${error.message}`);
        }
        // Optionally rethrow the error to let components handle it too
        return throwError(() => error);
      }));
  
};
