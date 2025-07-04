FROM node:22-alpine AS build

WORKDIR /app

COPY sampleApp/package.json .
COPY sampleApp/package-lock.json .

RUN npm install

COPY sampleApp/. .

RUN npm run build --prod

FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx","-g","daemon off;"]