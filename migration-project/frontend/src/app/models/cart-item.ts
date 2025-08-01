export class CartItem {
  product!: CartProduct;
  quantity!: number;
}

export interface CartProduct {
  productId: number;
  productName: string;
  price: number;
}
