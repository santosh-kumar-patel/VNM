import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { LoginRequest } from '../../APIModels/Request/login-request';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  private userService= new BehaviorSubject<LoginRequest | null>(null);
    loginRequest$=this.userService.asObservable();
    
    setUserName(loginObj: LoginRequest)
    {
      this.userService.next(loginObj);
    }

    
    private behavior = new BehaviorSubject<number>(0); // Initial value required
    private subject = new Subject<number>(); // Initial value required
    Check()
    {
      debugger;
      this.behavior.subscribe(val => console.log('A:', val)); // A: 0
      this.behavior.next(1); // A: 1

      this.behavior.subscribe(val => console.log('B:', val)); // B: 1
      this.behavior.next(2); // A: 2, B: 2

      this.subject.subscribe(val => console.log('C:', val));
      

      this.subject.subscribe(val => console.log('D:', val));
      this.subject.next(1); // A: 1
      this.subject.next(2); // A: 2, B: 2

    }

}
