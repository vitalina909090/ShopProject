import { baseApi } from './baseApi';

export const productApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getProduct: builder.query({
        query: (id) => `products/${id}`,
        providesTags: ['Product'],
    }),

  }),
});

export const {
  useGetProductQuery,
} = productApi;
