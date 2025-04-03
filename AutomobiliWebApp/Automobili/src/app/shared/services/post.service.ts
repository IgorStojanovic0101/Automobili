import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, lastValueFrom } from 'rxjs';
import { CrudService } from './crud-base.service';
import { SystemUrls } from '../utilities/system-urls';
import { Auto } from '../viewModels/auto';


@Injectable({
  providedIn: 'root',
})
export class AutoService implements CrudService<Auto> {
 
  private readonly http = inject(HttpClient);
  private readonly urls = inject(SystemUrls);

  getItems(): Observable<Auto[]> {

    return this.http.get<Auto[]>(this.urls.Autos.GetAutos);
  }

  getItemsAsPromise(searchRequest: Auto) {

    return lastValueFrom(this.http.post<Auto[]>(this.urls.Autos.GetAutos, searchRequest));
  }

  getItem(id: string) {
    return this.http.get<Auto>(`${this.urls.Autos.GetAutoById}/${id}`);
  }


  updateItem(value: Auto) {
    return this.http.put<Auto>(`${this.urls.Autos.UpdateAuto}`, value);
  }


  addItem(value: string): Observable<Auto> {
    throw new Error('Method not implemented.');
  }
  deleteItem(value: any): Observable<any> {
    throw new Error('Method not implemented.');
  }
 
}
