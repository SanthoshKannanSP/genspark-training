export class PaginatedResponse<T> {
  public items: T[] = [];
  public page: number = 0;
  public totalPage: number = 0;
}
