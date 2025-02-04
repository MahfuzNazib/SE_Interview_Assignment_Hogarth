export interface PaginationRequest {
    pageNumber: number;
    pageSize: number;
    searchValue?: string;
    orderBy?: string;
    isAscending?: boolean;
  }
