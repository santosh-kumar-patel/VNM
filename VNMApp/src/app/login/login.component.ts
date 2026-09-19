import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, signal } from '@angular/core';
import { AuthService } from '../Services/Auth_Service/auth.service';
import { CommonService } from '../Services/Common/common.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { LoginRequest } from '../APIModels/Request/login-request';
import { CommonModule } from '@angular/common';
import { EmployeeComponent } from "../employee/employee.component";


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, EmployeeComponent],
  templateUrl: './login.component.html',
//   styleUrl: './login.component.css'
   styleUrls: ['../../../node_modules/bootstrap/dist/css/bootstrap.min.css','./login.component.css'],
   changeDetection: ChangeDetectionStrategy.OnPush,

})
export class LoginComponent implements OnInit {

   loginReq!:LoginRequest;
   formData!:FormGroup;
   message:string='';
   data= signal<any>('');




   constructor(private authService : AuthService, private router : Router,
      private CommonService:CommonService
      // private cdref:ChangeDetectorRef
   ) { }


   ngOnInit() {
      setTimeout(() => {
         this.data.set('Hi Sammer!');
         //this.cdref.detectChanges();// Zone.js detects this and triggers change detection
      }, 1000);


      if (this.authService.isAuthenticated()) {
       this.router.navigate(['/employee']);
      }

      this.formData = new FormGroup({
         userName: new FormControl("",Validators.required),
         password: new FormControl("",Validators.required),
      });
   }


   // ReceiveMessage(msg:string)
   // {
   //    debugger;
   //    this.message=msg;
   // }

   onSubmit() {
      this.message='';

       if(this.formData.invalid)
      {
          if(this.formData.value.userName.trim()=="")
            this.message="Enter the User Name";
          else if(this.formData.value.password=="")
            this.message="Enter the Password";
      }
      if(this.formData.valid)
      {
         this.loginReq= new LoginRequest(this.formData.value.userName.trim(),this.formData.value.password);

      this.authService.login(this.loginReq)
         .subscribe({
          next: (response)=>{
              if(response)
               {
                  this.CommonService.Check();
                  //this.CommonService.setUserName(this.loginReq);
                this.router.navigate(['/employee']);
               }
              else
                this.message="Invalid User Credential!";

            },
             error: (err)=>console.log('error',err)
         });
      }

   }

}
