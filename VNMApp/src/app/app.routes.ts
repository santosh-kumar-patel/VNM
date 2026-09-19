import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { EmployeeComponent } from './employee/employee.component';
import { authGuard } from './Services/Auth_Guard/auth.guard';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    {path:'login',component:LoginComponent},
    {path:'employee',canActivate:[authGuard], component:EmployeeComponent
    //     loadComponent: () =>
    //   import('./employee/employee.component').then(m => m.EmployeeComponent)
    }
];
