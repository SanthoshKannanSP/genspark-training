FROM node:22-alpine AS build

WORKDIR /usr/src/app

COPY package.json .
COPY package-lock.json .

RUN npm install

COPY . .
RUN npm run build --prod

FROM nginx:alpine

COPY --from=build /usr/src/app/dist/productSale/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx","-g","daemon off;"]