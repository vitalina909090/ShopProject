export interface ProductColorDto {
  optionValueId: number;
  name: string;
  hex: string;
  imageUrl: string;
  price: number;
  discountedPrice: number | null;
  hasDiscount: boolean;
}

export interface CatalogItem {
  id: number;
  name: string;
  categoryName: string | null;
  isNew: boolean;
  isPopular: boolean;
  defaultVariantId: number;
  price: number;
  discountedPrice: number | null;
  imageUrl: string;
  colors: ProductColorDto[];
  hasDiscount: boolean;
  discountForm: number | null;
  discountValue: number | null;
}

export interface CatalogResponse {
  items: CatalogItem[];
  totalCount: number;
}