import { PaginationSummary } from './pagination-summary.model';

export interface ApiResponse<T> {
  status: boolean;
  message?: string;
  values?: T;
  paginationSummary?: PaginationSummary;
}
