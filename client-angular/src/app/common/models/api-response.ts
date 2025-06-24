export type ApiResponse<T> = {
  data: T;
  isSuccess: boolean;
  errorMessage?: string;
};

export type PagedApiResponse<T> = ApiResponse<T> & {
  currentPage: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
};
