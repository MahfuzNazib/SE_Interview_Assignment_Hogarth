import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DatasourceService {
  private dataSource = new BehaviorSubject<string>('MSSQL'); // Default Data Source
  dataSourceType$ = this.dataSource.asObservable();

  setDataSourceType(dataSourceType: string): void {
    console.log(dataSourceType);
    this.dataSource.next(dataSourceType);
  }

  getDataSourceType(): string {
    console.log('get type: ', this.dataSource.value)
    return this.dataSource.value;
  }
}
