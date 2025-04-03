import {environment} from "../../environments/environment.development";
import {Injectable} from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class SystemUrls
{
    private apiUrl:string = environment.Url + '/api'
   

    public get Autos() {
        const pref = this.apiUrl + '/auto';
        return {
            GetAutos: `${pref}/get-autos`,  
            GetAutoById: `${pref}/get-auto-by-id`, 
            UpdateAuto: `${pref}/update-auto`, 
            
        };
    }


   



}