import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatasourceService } from '../../core/services/header_datasource/datasource.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit {
  selectedDatabase: string = 'MSSQL';

  constructor(private dataSourceService: DatasourceService) {}

  ngOnInit(): void {
    this.selectedDatabase = this.dataSourceService.getDataSourceType();
  }

  onDataSourceChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedDatabase = target.value;
    this.dataSourceService.setDataSourceType(target.value);
    window.location.reload();
  }
}
