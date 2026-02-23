# !!!! Use this file only for DEVELOPMENT !!!!

FROM node:24-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 3420

CMD ["npm", "run", "dev", "--", "--host", "--port", "3420"]