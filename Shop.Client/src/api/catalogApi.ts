import { baseApi } from './baseApi';
import type { CatalogResponse } from '../types/types';

export interface CatalogParams {
  page?: number;
  pageSize?: number;
}

export const catalogApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    productCatalog: builder.query<CatalogResponse, CatalogParams>({
      query: (params) => ({ url: 'catalog/products', params }),
      providesTags: ['Product'],
    }),
  }),
});

export const { useProductCatalogQuery } = catalogApi;