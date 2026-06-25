import { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faImage, faSpinner } from '@fortawesome/free-solid-svg-icons';
import type { CatalogItem, ProductColorDto } from '../../types/types';
import styles from './ProductCard.module.css';

export default function ProductCard({ item }: { item: CatalogItem }) {
  const defaultColor = item.colors.length > 0 ? item.colors[0] : null;

  const [currentImage, setCurrentImage] = useState<string>(
    defaultColor?.imageUrl || item.imageUrl
  );
  const [activeColor, setActiveColor] = useState<ProductColorDto | null>(defaultColor);
  const [isLoading, setIsLoading] = useState(false);
  const [imageError, setImageError] = useState(false);

  const displayPrice = activeColor?.price ?? item.price;
  const displayDiscountedPrice = activeColor?.discountedPrice ?? item.discountedPrice;
  const displayHasDiscount = activeColor?.hasDiscount ?? item.hasDiscount;

  const selectColor = (color: ProductColorDto) => {
    setActiveColor(color);
    setIsLoading(true);
    setImageError(false);

    const url = color.imageUrl || item.imageUrl;
    if (url && url !== currentImage) {
      setCurrentImage(url);
    } else {
      setIsLoading(false);
    }
  };

  const handleColorClick = (e: React.MouseEvent, color: ProductColorDto) => {
    e.preventDefault();
    e.stopPropagation();
    selectColor(color);
  };

  const handleColorKeyDown = (e: React.KeyboardEvent, color: ProductColorDto) => {
    if (e.key === 'Enter' || e.key === ' ') {
      e.preventDefault();
      e.stopPropagation();
      selectColor(color);
    }
  };

  return (
    <Link to={`/products/${item.id}`} className={styles.card}>
      <div className={styles.imageWrap}>
        {isLoading && (
          <div className={styles.loader}>
            <FontAwesomeIcon icon={faSpinner} spin />
          </div>
        )}

        {currentImage && !imageError ? (
          <img
            src={currentImage}
            alt={item.name}
            className={styles.image}
            onLoad={() => setIsLoading(false)}
            onError={() => { setIsLoading(false); setImageError(true); }}
          />
        ) : (
          <div className={styles.placeholder}>
            <FontAwesomeIcon icon={faImage} />
          </div>
        )}

        {displayHasDiscount && (
          <span className={styles.saleBadge}>Sale</span>
        )}
      </div>

      <div className={styles.name}>{item.name}</div>

      <div className={styles.prices}>
        {displayHasDiscount && displayDiscountedPrice != null ? (
          <>
            <span className={styles.oldPrice}>${displayPrice.toFixed(2)}</span>
            <span className={styles.price}>${displayDiscountedPrice.toFixed(2)}</span>
          </>
        ) : (
          <span className={`${styles.price} ${styles.priceRegular}`}>
            ${displayPrice.toFixed(2)}
          </span>
        )}
      </div>

      {item.colors.length > 0 && (
        <div className={styles.swatches}>
          {item.colors.map((color) => (
            <span
              key={color.optionValueId}
              role="button"
              tabIndex={0}
              aria-label={color.name}
              className={[
                styles.swatch,
                activeColor?.optionValueId === color.optionValueId
                  ? styles.swatchActive
                  : '',
              ].join(' ')}
              style={{ backgroundColor: color.hex }}
              onClick={(e) => handleColorClick(e, color)}
              onKeyDown={(e) => handleColorKeyDown(e, color)}
            />
          ))}
        </div>
      )}
    </Link>
  );
}