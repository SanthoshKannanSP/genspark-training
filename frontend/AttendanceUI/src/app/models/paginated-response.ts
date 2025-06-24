export class PaginatedResponse<T> {
  public pagination: Pagination | null = null;
  public data: T | null = null;
}

class Pagination {
  public totalRecords!: number;
  public page!: number;
  public pageSize!: number;
  public totalPages!: number;
}
