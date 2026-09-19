import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'empPipe',
  standalone: true
})
export class EmpPipePipe implements PipeTransform {

  result : string[]=[];
  transform(value : string): string {
    this.result=[];
   
    let quotient: number=parseInt(value);
    for(let i:number=0;quotient>0; i++)
    {
      let reminder : number= quotient%10;
      quotient=Math.trunc(quotient/10);
      if(reminder ==1)
         this.result.push('One');
      if(reminder ==2)
         this.result.push('Two');
      if(reminder ==3)
         this.result.push('Three');
      if(reminder ==4)
         this.result.push('Four');
      if(reminder ==5)
         this.result.push('Five');
      if(reminder ==6)
         this.result.push('Six');
      if(reminder ==7)
         this.result.push('Seven');
      if(reminder ==8)
         this.result.push('Eight');
      if(reminder ==9)
         this.result.push('Nine');
      if(reminder ==0)
         this.result.push('Zero');

    }
      let joined:string= this.result.join(" ").split(" ").reverse().join(" ");
      return joined;
    
  }

}
