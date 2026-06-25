import { useProductCatalogQuery } from '../../api/catalogApi';
import ProductCard from '../../components/ProductCard/ProductCard';
import styles from './CatalogPage.module.css';

function CatalogPage() {
  const { data: catalogData, isLoading, isError } = useProductCatalogQuery({});

  const catalogItems = catalogData?.items ?? [];

  return (
    <div className={styles.page}>
      <div className={styles.main}>
        {(isLoading || isError) && (
          <div className="feedback-wrap">
            {isLoading && <span className="loading">Loading...</span>}
            {isError && <span className="error-msg">Failed to load products</span>}
          </div>
        )}

        {!isLoading && !isError && catalogItems.length === 0 && (
          <div className="empty-state">No products found</div>
        )}

        {catalogItems.length > 0 && (
          <div className={styles.grid}>
            {catalogItems.map((item) => (
              <ProductCard key={item.id} item={item} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default CatalogPage;