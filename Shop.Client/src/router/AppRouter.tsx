import { createBrowserRouter, RouterProvider, Navigate } from 'react-router-dom';
import Layout from '../layouts/Layout';
import CatalogPage from '../pages/catalog/CatalogPage';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    children: [
      { 
        index: true,
        element: <Navigate to="/catalog" replace /> 
      },
      { 
        path: 'catalog',
        element: <CatalogPage /> 
      },
    ],
  },
]);

export default function AppRouter() {
  return <RouterProvider router={router} />;
}
