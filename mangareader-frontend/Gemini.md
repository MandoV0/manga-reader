{
  "openapi": "3.0.4",
  "info": {
    "title": "MangaReader API",
    "version": "v1"
  },
  "paths": {
    "/api/v1/Auth/register": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/RegisterRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Auth/login": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/TokenDto"
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Auth/forgot-password": {
      "post": {
        "tags": [
          "Auth"
        ],
        "parameters": [
          {
            "name": "email",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/v1/Auth/reset-password": {
      "post": {
        "tags": [
          "Auth"
        ],
        "parameters": [
          {
            "name": "token",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "email",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "type": "string"
              }
            },
            "text/json": {
              "schema": {
                "type": "string"
              }
            },
            "application/*+json": {
              "schema": {
                "type": "string"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/v1/Genres": {
      "get": {
        "tags": [
          "Genres"
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/Genre"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/Genre"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/Genre"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/series/{seriesId}/rate": {
      "post": {
        "tags": [
          "Rating"
        ],
        "parameters": [
          {
            "name": "seriesId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "put": {
        "tags": [
          "Rating"
        ],
        "parameters": [
          {
            "name": "seriesId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/CreateRatingDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "delete": {
        "tags": [
          "Rating"
        ],
        "parameters": [
          {
            "name": "seriesId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/v1/Series/{id}": {
      "get": {
        "tags": [
          "Series"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesDto"
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series": {
      "get": {
        "tags": [
          "Series"
        ],
        "parameters": [
          {
            "name": "page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32",
              "default": 1
            }
          },
          {
            "name": "pageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32",
              "default": 20
            }
          },
          {
            "name": "sort",
            "in": "query",
            "schema": {
              "type": "string",
              "default": ""
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesListDtoPagedResponseDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesListDtoPagedResponseDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/SeriesListDtoPagedResponseDto"
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/{id}/chapters": {
      "get": {
        "tags": [
          "Series"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ChapterDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ChapterDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/ChapterDto"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/trending": {
      "get": {
        "tags": [
          "Series"
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/popular": {
      "get": {
        "tags": [
          "Series"
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/recently-updated": {
      "get": {
        "tags": [
          "Series"
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/SeriesListDto"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/last-read": {
      "post": {
        "tags": [
          "Series"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateLastReadChapterDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateLastReadChapterDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateLastReadChapterDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      },
      "get": {
        "tags": [
          "Series"
        ],
        "parameters": [
          {
            "name": "limit",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32",
              "default": 10
            }
          },
          {
            "name": "offset",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32",
              "default": 0
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/LastReadChapterDto"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/LastReadChapterDto"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/LastReadChapterDto"
                  }
                }
              }
            }
          }
        }
      }
    },
    "/api/v1/Series/chapters/{chapterId}": {
      "get": {
        "tags": [
          "Series"
        ],
        "parameters": [
          {
            "name": "chapterId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/ChapterDto"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/ChapterDto"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/ChapterDto"
                }
              }
            }
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "Chapter": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "seriesId": {
            "type": "integer",
            "format": "int32"
          },
          "series": {
            "$ref": "#/components/schemas/Series"
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "viewsCount": {
            "type": "integer",
            "format": "int32"
          },
          "pages": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/Page"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "ChapterDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "pages": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/PageDto"
            },
            "nullable": true
          },
          "pagesCount": {
            "type": "integer",
            "format": "int32",
            "readOnly": true
          },
          "viewsCount": {
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      },
      "CreateRatingDto": {
        "required": [
          "rating"
        ],
        "type": "object",
        "properties": {
          "rating": {
            "maximum": 10,
            "minimum": 0,
            "type": "integer",
            "format": "int32"
          },
          "comment": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "Genre": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "name": {
            "type": "string",
            "nullable": true
          },
          "series": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/Series"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "LastReadChapterDto": {
        "type": "object",
        "properties": {
          "seriesId": {
            "type": "integer",
            "format": "int32"
          },
          "chapterId": {
            "type": "integer",
            "format": "int32"
          },
          "seriesTitle": {
            "type": "string",
            "nullable": true
          },
          "chapterTitle": {
            "type": "string",
            "nullable": true
          },
          "coverImageUrl": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "LoginRequestDto": {
        "required": [
          "email",
          "password"
        ],
        "type": "object",
        "properties": {
          "email": {
            "minLength": 1,
            "type": "string",
            "format": "email"
          },
          "password": {
            "minLength": 1,
            "type": "string"
          }
        },
        "additionalProperties": false
      },
      "Page": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "chapterId": {
            "type": "integer",
            "format": "int32"
          },
          "chapter": {
            "$ref": "#/components/schemas/Chapter"
          },
          "index": {
            "type": "integer",
            "format": "int32"
          },
          "imageUrl": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "PageDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "pageNumber": {
            "type": "integer",
            "format": "int32"
          },
          "imageUrl": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "RegisterRequestDto": {
        "required": [
          "email",
          "password",
          "username"
        ],
        "type": "object",
        "properties": {
          "username": {
            "maxLength": 50,
            "minLength": 3,
            "type": "string"
          },
          "email": {
            "minLength": 1,
            "type": "string",
            "format": "email"
          },
          "password": {
            "maxLength": 100,
            "minLength": 8,
            "type": "string"
          }
        },
        "additionalProperties": false
      },
      "Series": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "author": {
            "type": "string",
            "nullable": true
          },
          "publisher": {
            "type": "string",
            "nullable": true
          },
          "viewsCount": {
            "type": "integer",
            "format": "int32"
          },
          "status": {
            "$ref": "#/components/schemas/SeriesStatus"
          },
          "genres": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/Genre"
            },
            "nullable": true
          },
          "coverImageUrl": {
            "type": "string",
            "nullable": true
          },
          "releaseDate": {
            "type": "string",
            "format": "date-time"
          },
          "totalRatings": {
            "type": "integer",
            "format": "int32"
          },
          "totalRatingSum": {
            "type": "integer",
            "format": "int32"
          },
          "averageRating": {
            "type": "number",
            "format": "double"
          },
          "chapters": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/Chapter"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "SeriesDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "coverImageUrl": {
            "type": "string",
            "nullable": true
          },
          "author": {
            "type": "string",
            "nullable": true
          },
          "publisher": {
            "type": "string",
            "nullable": true
          },
          "status": {
            "type": "string",
            "nullable": true
          },
          "genres": {
            "type": "array",
            "items": {
              "type": "string"
            },
            "nullable": true
          },
          "releaseDate": {
            "type": "string",
            "format": "date-time"
          },
          "averageRating": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "SeriesListDto": {
        "required": [
          "title"
        ],
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "coverImageUrl": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "SeriesListDtoPagedResponseDto": {
        "type": "object",
        "properties": {
          "items": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/SeriesListDto"
            },
            "nullable": true
          },
          "page": {
            "type": "integer",
            "format": "int32"
          },
          "pageSize": {
            "type": "integer",
            "format": "int32"
          },
          "totalCount": {
            "type": "integer",
            "format": "int32"
          },
          "totalPages": {
            "type": "integer",
            "format": "int32",
            "readOnly": true
          },
          "hasNextPage": {
            "type": "boolean",
            "readOnly": true
          },
          "hasPreviousPage": {
            "type": "boolean",
            "readOnly": true
          }
        },
        "additionalProperties": false
      },
      "SeriesStatus": {
        "enum": [
          0,
          1,
          2,
          3
        ],
        "type": "integer",
        "format": "int32"
      },
      "TokenDto": {
        "type": "object",
        "properties": {
          "token": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "UpdateLastReadChapterDto": {
        "required": [
          "lastReadChapterId",
          "seriesId"
        ],
        "type": "object",
        "properties": {
          "seriesId": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          },
          "lastReadChapterId": {
            "maximum": 2147483647,
            "minimum": 1,
            "type": "integer",
            "format": "int32"
          }
        },
        "additionalProperties": false
      }
    },
    "securitySchemes": {
      "Bearer": {
        "type": "http",
        "description": "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        "scheme": "Bearer",
        "bearerFormat": "JWT"
      }
    }
  },
  "security": [
    {
      "Bearer": [ ]
    }
  ]
}