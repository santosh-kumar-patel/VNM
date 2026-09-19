import { Component, EventEmitter, Input, input, NgModule, OnInit, Output, output } from '@angular/core';
import { AuthService } from '../Services/Auth_Service/auth.service';
import { CommonService } from '../Services/Common/common.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { EmpPipePipe } from '../Pipe/emp-pipe.pipe';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [FormsModule, EmpPipePipe,CommonModule ],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent {


  constructor(private authService : AuthService, private router : Router,private commonService:CommonService) { 
  }


  userName?:string;
   ngOnInit()
   {
    this.commonService.loginRequest$.subscribe(obj=>{
      this.userName=obj?.UserName;

    });
   }
 //@Input() userName!:string;
@Output() messageEvent= new EventEmitter<string>();
  message: string='';
  Logout()
  {
      this.authService.logout();
      this.messageEvent.emit("Logout Successfully!");

       this.router.navigate(['/login']);

  }

  employees = [
  { id: 1, name: 'Santosh' },
  { id: 2, name: 'Sameer' },
  { id: 3, name: 'Rakhi' },
  { id: 2, name: 'Naveen' }
];

trackByEmpId(index: number, item: any): number {
  return item.id;
}

deleteEmployee(empID:number)
{
  this.employees = this.employees.map(emp => ({
      ...emp,
      name: emp.name + ' (Updated)'
    }));

 // this.employees=this.employees.filter(emp => emp.id !== empID);
}

addEmployee(empName:string)
{
  this.employees=[...this.employees,{id:this.getLastId()+1, name:empName}]
}

getLastId(): number {
  return this.employees.length ? this.employees[this.employees.length - 1].id : 0;
}


}
