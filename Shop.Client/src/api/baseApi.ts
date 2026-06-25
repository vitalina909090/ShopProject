import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const baseApi = createApi({
  reducerPath: 'baseApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:7088/api',
  }),
  tagTypes: ['Product'],
  endpoints: () => ({}),
});
