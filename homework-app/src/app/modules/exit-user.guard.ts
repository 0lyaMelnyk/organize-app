import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

export interface ComponentCanDeactivate{
  canDeactivate: () => boolean | Observable<boolean>;
}
@Injectable({
  providedIn: 'root'
})
export class ExitUserGuard implements CanDeactivate<ComponentCanDeactivate> {
  saved: boolean = false;
    save(){
        this.saved = true;
    }
  canDeactivate(component: ComponentCanDeactivate) : Observable<boolean> | boolean{
         
    if(!this.saved){
      return confirm("Вы хотите покинуть страницу?");
  }
  else{
      return true;
  }}
  
}
