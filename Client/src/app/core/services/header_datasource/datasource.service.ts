import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DatasourceService {
  private dataSource = new BehaviorSubject<string>(this.getStoredDataSource());
  dataSourceType$ = this.dataSource.asObservable();

  constructor() {}

  setDataSourceType(dataSourceType: string): void {
    localStorage.setItem('selectedDatabase', dataSourceType);
    this.dataSource.next(dataSourceType);
  }

  getDataSourceType(): string {
    return this.dataSource.value;
  }

  private getStoredDataSource(): string {
    return localStorage.getItem('selectedDatabase') || 'MSSQL';
  }
}
