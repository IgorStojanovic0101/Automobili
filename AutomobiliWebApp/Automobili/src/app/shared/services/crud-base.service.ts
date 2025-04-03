import { Observable } from 'rxjs';

export interface CrudService<T,R = any> {
  getItems(): Observable<T[]>;

  getItemsAsPromise(request?: R): Promise<T[]>;

  getItem(id: string): Observable<T>;

  addItem(value: string): Observable<T>;

  updateItem(value: R): Observable<T>;

  deleteItem(value: R): Observable<any>;
}
