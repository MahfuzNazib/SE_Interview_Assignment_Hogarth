export interface PaginationSummary {
    page: number;
    perPage: number;
    totalCount: number;
    firstPage: number;
    lastPage: number;
    orderBy: string;
    ascending: boolean;
    searchParam?: string;
  }