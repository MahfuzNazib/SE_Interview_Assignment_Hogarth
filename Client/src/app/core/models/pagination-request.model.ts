export interface PaginationRequest {
    page: number;
    perPage: number;
    orderBy?: string;
    ascending?: boolean;
    searchParam?: string;
  }
  