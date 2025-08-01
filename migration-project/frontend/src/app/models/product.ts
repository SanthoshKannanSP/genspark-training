import { CartProduct } from './cart-item';

export class Product implements CartProduct {
  productId: number = 0;
  productName: string = '';
  image: string = '';
  price: number = 0;
  userId: number = 0;
  categoryId: number = 0;
  colorId: number = 0;
  modelId: number = 0;
  storageId: number = 0;
  sellStartDate!: Date;
  sellEndDate!: Date;
  isNew: number = 0;
}
