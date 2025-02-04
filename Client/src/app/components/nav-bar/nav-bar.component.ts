import { Component } from '@angular/core';
import { DatasourceService } from '../../core/services/header_datasource/datasource.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  selectedDatabase: string = 'MSSQL'; 

  constructor(private dataSourceService: DatasourceService){}

  onDataSourceChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedDatabase = target.value;
    this.dataSourceService.setDataSourceType(target.value);
  }
}
