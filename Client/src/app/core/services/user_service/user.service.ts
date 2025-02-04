import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DatasourceService } from '../header_datasource/datasource.service';
import { PaginationRequest } from '../../models/pagination-request.model';
import { ApiResponse } from '../../models/api-response.model';
import { User } from '../../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseApiUrl = environment.baseURL; // Ensure this is defined in environment.ts

  constructor(
    private http: HttpClient,
    private dataSourceService: DatasourceService
  ) {}


  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Database-Type': this.dataSourceService.getDataSourceType(), // Get the current data source type
    });
  }


  GetUsers(paginationObj: PaginationRequest): Observable<ApiResponse<User[]>> {
    const headers = this.getHeaders(); 

    return this.http
      .post<ApiResponse<User[]>>(
        `${this.baseApiUrl}/User/GetUsers`,
        paginationObj,
        { headers }
      )
      .pipe(
        catchError((error) => {
          console.error('Error fetching users', error);
          throw error; 
        })
      );
  }
}