-- Postgres seed for MangaReaderAPI
-- Data-only seed. Assumes tables already exist.

-- Seed data
-- 100 series with 3 chapters each (first has 10 pages, next 2 have 8 pages)
INSERT INTO series (title, description, author, publisher, status, genres, release_date, average_rating, cover_image)
VALUES
-- s1..s100
('Series 1', 'Description for Series 1', 'Author 1', 'Publisher 1', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2015-01-01', 4.2, 'https://placehold.co/200x300'),
('Series 2', 'Description for Series 2', 'Author 2', 'Publisher 2', 'Completed', ARRAY['Adventure','Drama'], DATE '2015-02-01', 3.9, 'https://placehold.co/200x300'),
('Series 3', 'Description for Series 3', 'Author 3', 'Publisher 3', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2015-03-01', 4.5, 'https://placehold.co/200x300'),
('Series 4', 'Description for Series 4', 'Author 4', 'Publisher 4', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2015-04-01', 4.1, 'https://placehold.co/200x300'),
('Series 5', 'Description for Series 5', 'Author 5', 'Publisher 5', 'Completed', ARRAY['Romance','Drama'], DATE '2015-05-01', 3.8, 'https://placehold.co/200x300'),
('Series 6', 'Description for Series 6', 'Author 6', 'Publisher 6', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2015-06-01', 4.0, 'https://placehold.co/200x300'),
('Series 7', 'Description for Series 7', 'Author 7', 'Publisher 7', 'Ongoing', ARRAY['Action','Adventure'], DATE '2015-07-01', 4.3, 'https://placehold.co/200x300'),
('Series 8', 'Description for Series 8', 'Author 8', 'Publisher 8', 'Cancelled', ARRAY['Drama'], DATE '2015-08-01', 2.9, 'https://placehold.co/200x300'),
('Series 9', 'Description for Series 9', 'Author 9', 'Publisher 9', 'Ongoing', ARRAY['Fantasy'], DATE '2015-09-01', 4.6, 'https://placehold.co/200x300'),
('Series 10', 'Description for Series 10', 'Author 10', 'Publisher 10', 'Completed', ARRAY['Action','Comedy'], DATE '2015-10-01', 4.0, 'https://placehold.co/200x300'),
('Series 11', 'Description for Series 11', 'Author 11', 'Publisher 11', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2015-11-01', 4.2, 'https://placehold.co/200x300'),
('Series 12', 'Description for Series 12', 'Author 12', 'Publisher 12', 'Completed', ARRAY['Adventure','Drama'], DATE '2015-12-01', 3.9, 'https://placehold.co/200x300'),
('Series 13', 'Description for Series 13', 'Author 13', 'Publisher 13', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2016-01-01', 4.5, 'https://placehold.co/200x300'),
('Series 14', 'Description for Series 14', 'Author 14', 'Publisher 14', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2016-02-01', 4.1, 'https://placehold.co/200x300'),
('Series 15', 'Description for Series 15', 'Author 15', 'Publisher 15', 'Completed', ARRAY['Romance','Drama'], DATE '2016-03-01', 3.8, 'https://placehold.co/200x300'),
('Series 16', 'Description for Series 16', 'Author 16', 'Publisher 16', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2016-04-01', 4.0, 'https://placehold.co/200x300'),
('Series 17', 'Description for Series 17', 'Author 17', 'Publisher 17', 'Ongoing', ARRAY['Action','Adventure'], DATE '2016-05-01', 4.3, 'https://placehold.co/200x300'),
('Series 18', 'Description for Series 18', 'Author 18', 'Publisher 18', 'Cancelled', ARRAY['Drama'], DATE '2016-06-01', 2.9, 'https://placehold.co/200x300'),
('Series 19', 'Description for Series 19', 'Author 19', 'Publisher 19', 'Ongoing', ARRAY['Fantasy'], DATE '2016-07-01', 4.6, 'https://placehold.co/200x300'),
('Series 20', 'Description for Series 20', 'Author 20', 'Publisher 20', 'Completed', ARRAY['Action','Comedy'], DATE '2016-08-01', 4.0, 'https://placehold.co/200x300'),
('Series 21', 'Description for Series 21', 'Author 21', 'Publisher 21', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2016-09-01', 4.2, 'https://placehold.co/200x300'),
('Series 22', 'Description for Series 22', 'Author 22', 'Publisher 22', 'Completed', ARRAY['Adventure','Drama'], DATE '2016-10-01', 3.9, 'https://placehold.co/200x300'),
('Series 23', 'Description for Series 23', 'Author 23', 'Publisher 23', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2016-11-01', 4.5, 'https://placehold.co/200x300'),
('Series 24', 'Description for Series 24', 'Author 24', 'Publisher 24', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2016-12-01', 4.1, 'https://placehold.co/200x300'),
('Series 25', 'Description for Series 25', 'Author 25', 'Publisher 25', 'Completed', ARRAY['Romance','Drama'], DATE '2017-01-01', 3.8, 'https://placehold.co/200x300'),
('Series 26', 'Description for Series 26', 'Author 26', 'Publisher 26', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2017-02-01', 4.0, 'https://placehold.co/200x300'),
('Series 27', 'Description for Series 27', 'Author 27', 'Publisher 27', 'Ongoing', ARRAY['Action','Adventure'], DATE '2017-03-01', 4.3, 'https://placehold.co/200x300'),
('Series 28', 'Description for Series 28', 'Author 28', 'Publisher 28', 'Cancelled', ARRAY['Drama'], DATE '2017-04-01', 2.9, 'https://placehold.co/200x300'),
('Series 29', 'Description for Series 29', 'Author 29', 'Publisher 29', 'Ongoing', ARRAY['Fantasy'], DATE '2017-05-01', 4.6, 'https://placehold.co/200x300'),
('Series 30', 'Description for Series 30', 'Author 30', 'Publisher 30', 'Completed', ARRAY['Action','Comedy'], DATE '2017-06-01', 4.0, 'https://placehold.co/200x300'),
('Series 31', 'Description for Series 31', 'Author 31', 'Publisher 31', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2017-07-01', 4.2, 'https://placehold.co/200x300'),
('Series 32', 'Description for Series 32', 'Author 32', 'Publisher 32', 'Completed', ARRAY['Adventure','Drama'], DATE '2017-08-01', 3.9, 'https://placehold.co/200x300'),
('Series 33', 'Description for Series 33', 'Author 33', 'Publisher 33', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2017-09-01', 4.5, 'https://placehold.co/200x300'),
('Series 34', 'Description for Series 34', 'Author 34', 'Publisher 34', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2017-10-01', 4.1, 'https://placehold.co/200x300'),
('Series 35', 'Description for Series 35', 'Author 35', 'Publisher 35', 'Completed', ARRAY['Romance','Drama'], DATE '2017-11-01', 3.8, 'https://placehold.co/200x300'),
('Series 36', 'Description for Series 36', 'Author 36', 'Publisher 36', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2017-12-01', 4.0, 'https://placehold.co/200x300'),
('Series 37', 'Description for Series 37', 'Author 37', 'Publisher 37', 'Ongoing', ARRAY['Action','Adventure'], DATE '2018-01-01', 4.3, 'https://placehold.co/200x300'),
('Series 38', 'Description for Series 38', 'Author 38', 'Publisher 38', 'Cancelled', ARRAY['Drama'], DATE '2018-02-01', 2.9, 'https://placehold.co/200x300'),
('Series 39', 'Description for Series 39', 'Author 39', 'Publisher 39', 'Ongoing', ARRAY['Fantasy'], DATE '2018-03-01', 4.6, 'https://placehold.co/200x300'),
('Series 40', 'Description for Series 40', 'Author 40', 'Publisher 40', 'Completed', ARRAY['Action','Comedy'], DATE '2018-04-01', 4.0, 'https://placehold.co/200x300'),
('Series 41', 'Description for Series 41', 'Author 41', 'Publisher 41', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2018-05-01', 4.2, 'https://placehold.co/200x300'),
('Series 42', 'Description for Series 42', 'Author 42', 'Publisher 42', 'Completed', ARRAY['Adventure','Drama'], DATE '2018-06-01', 3.9, 'https://placehold.co/200x300'),
('Series 43', 'Description for Series 43', 'Author 43', 'Publisher 43', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2018-07-01', 4.5, 'https://placehold.co/200x300'),
('Series 44', 'Description for Series 44', 'Author 44', 'Publisher 44', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2018-08-01', 4.1, 'https://placehold.co/200x300'),
('Series 45', 'Description for Series 45', 'Author 45', 'Publisher 45', 'Completed', ARRAY['Romance','Drama'], DATE '2018-09-01', 3.8, 'https://placehold.co/200x300'),
('Series 46', 'Description for Series 46', 'Author 46', 'Publisher 46', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2018-10-01', 4.0, 'https://placehold.co/200x300'),
('Series 47', 'Description for Series 47', 'Author 47', 'Publisher 47', 'Ongoing', ARRAY['Action','Adventure'], DATE '2018-11-01', 4.3, 'https://placehold.co/200x300'),
('Series 48', 'Description for Series 48', 'Author 48', 'Publisher 48', 'Cancelled', ARRAY['Drama'], DATE '2018-12-01', 2.9, 'https://placehold.co/200x300'),
('Series 49', 'Description for Series 49', 'Author 49', 'Publisher 49', 'Ongoing', ARRAY['Fantasy'], DATE '2019-01-01', 4.6, 'https://placehold.co/200x300'),
('Series 50', 'Description for Series 50', 'Author 50', 'Publisher 50', 'Completed', ARRAY['Action','Comedy'], DATE '2019-02-01', 4.0, 'https://placehold.co/200x300'),
('Series 51', 'Description for Series 51', 'Author 51', 'Publisher 51', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2019-03-01', 4.2, 'https://placehold.co/200x300'),
('Series 52', 'Description for Series 52', 'Author 52', 'Publisher 52', 'Completed', ARRAY['Adventure','Drama'], DATE '2019-04-01', 3.9, 'https://placehold.co/200x300'),
('Series 53', 'Description for Series 53', 'Author 53', 'Publisher 53', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2019-05-01', 4.5, 'https://placehold.co/200x300'),
('Series 54', 'Description for Series 54', 'Author 54', 'Publisher 54', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2019-06-01', 4.1, 'https://placehold.co/200x300'),
('Series 55', 'Description for Series 55', 'Author 55', 'Publisher 55', 'Completed', ARRAY['Romance','Drama'], DATE '2019-07-01', 3.8, 'https://placehold.co/200x300'),
('Series 56', 'Description for Series 56', 'Author 56', 'Publisher 56', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2019-08-01', 4.0, 'https://placehold.co/200x300'),
('Series 57', 'Description for Series 57', 'Author 57', 'Publisher 57', 'Ongoing', ARRAY['Action','Adventure'], DATE '2019-09-01', 4.3, 'https://placehold.co/200x300'),
('Series 58', 'Description for Series 58', 'Author 58', 'Publisher 58', 'Cancelled', ARRAY['Drama'], DATE '2019-10-01', 2.9, 'https://placehold.co/200x300'),
('Series 59', 'Description for Series 59', 'Author 59', 'Publisher 59', 'Ongoing', ARRAY['Fantasy'], DATE '2019-11-01', 4.6, 'https://placehold.co/200x300'),
('Series 60', 'Description for Series 60', 'Author 60', 'Publisher 60', 'Completed', ARRAY['Action','Comedy'], DATE '2019-12-01', 4.0, 'https://placehold.co/200x300'),
('Series 61', 'Description for Series 61', 'Author 61', 'Publisher 61', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2020-01-01', 4.2, 'https://placehold.co/200x300'),
('Series 62', 'Description for Series 62', 'Author 62', 'Publisher 62', 'Completed', ARRAY['Adventure','Drama'], DATE '2020-02-01', 3.9, 'https://placehold.co/200x300'),
('Series 63', 'Description for Series 63', 'Author 63', 'Publisher 63', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2020-03-01', 4.5, 'https://placehold.co/200x300'),
('Series 64', 'Description for Series 64', 'Author 64', 'Publisher 64', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2020-04-01', 4.1, 'https://placehold.co/200x300'),
('Series 65', 'Description for Series 65', 'Author 65', 'Publisher 65', 'Completed', ARRAY['Romance','Drama'], DATE '2020-05-01', 3.8, 'https://placehold.co/200x300'),
('Series 66', 'Description for Series 66', 'Author 66', 'Publisher 66', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2020-06-01', 4.0, 'https://placehold.co/200x300'),
('Series 67', 'Description for Series 67', 'Author 67', 'Publisher 67', 'Ongoing', ARRAY['Action','Adventure'], DATE '2020-07-01', 4.3, 'https://placehold.co/200x300'),
('Series 68', 'Description for Series 68', 'Author 68', 'Publisher 68', 'Cancelled', ARRAY['Drama'], DATE '2020-08-01', 2.9, 'https://placehold.co/200x300'),
('Series 69', 'Description for Series 69', 'Author 69', 'Publisher 69', 'Ongoing', ARRAY['Fantasy'], DATE '2020-09-01', 4.6, 'https://placehold.co/200x300'),
('Series 70', 'Description for Series 70', 'Author 70', 'Publisher 70', 'Completed', ARRAY['Action','Comedy'], DATE '2020-10-01', 4.0, 'https://placehold.co/200x300'),
('Series 71', 'Description for Series 71', 'Author 71', 'Publisher 71', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2020-11-01', 4.2, 'https://placehold.co/200x300'),
('Series 72', 'Description for Series 72', 'Author 72', 'Publisher 72', 'Completed', ARRAY['Adventure','Drama'], DATE '2020-12-01', 3.9, 'https://placehold.co/200x300'),
('Series 73', 'Description for Series 73', 'Author 73', 'Publisher 73', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2021-01-01', 4.5, 'https://placehold.co/200x300'),
('Series 74', 'Description for Series 74', 'Author 74', 'Publisher 74', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2021-02-01', 4.1, 'https://placehold.co/200x300'),
('Series 75', 'Description for Series 75', 'Author 75', 'Publisher 75', 'Completed', ARRAY['Romance','Drama'], DATE '2021-03-01', 3.8, 'https://placehold.co/200x300'),
('Series 76', 'Description for Series 76', 'Author 76', 'Publisher 76', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2021-04-01', 4.0, 'https://placehold.co/200x300'),
('Series 77', 'Description for Series 77', 'Author 77', 'Publisher 77', 'Ongoing', ARRAY['Action','Adventure'], DATE '2021-05-01', 4.3, 'https://placehold.co/200x300'),
('Series 78', 'Description for Series 78', 'Author 78', 'Publisher 78', 'Cancelled', ARRAY['Drama'], DATE '2021-06-01', 2.9, 'https://placehold.co/200x300'),
('Series 79', 'Description for Series 79', 'Author 79', 'Publisher 79', 'Ongoing', ARRAY['Fantasy'], DATE '2021-07-01', 4.6, 'https://placehold.co/200x300'),
('Series 80', 'Description for Series 80', 'Author 80', 'Publisher 80', 'Completed', ARRAY['Action','Comedy'], DATE '2021-08-01', 4.0, 'https://placehold.co/200x300'),
('Series 81', 'Description for Series 81', 'Author 81', 'Publisher 81', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2021-09-01', 4.2, 'https://placehold.co/200x300'),
('Series 82', 'Description for Series 82', 'Author 82', 'Publisher 82', 'Completed', ARRAY['Adventure','Drama'], DATE '2021-10-01', 3.9, 'https://placehold.co/200x300'),
('Series 83', 'Description for Series 83', 'Author 83', 'Publisher 83', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2021-11-01', 4.5, 'https://placehold.co/200x300'),
('Series 84', 'Description for Series 84', 'Author 84', 'Publisher 84', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2021-12-01', 4.1, 'https://placehold.co/200x300'),
('Series 85', 'Description for Series 85', 'Author 85', 'Publisher 85', 'Completed', ARRAY['Romance','Drama'], DATE '2022-01-01', 3.8, 'https://placehold.co/200x300'),
('Series 86', 'Description for Series 86', 'Author 86', 'Publisher 86', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2022-02-01', 4.0, 'https://placehold.co/200x300'),
('Series 87', 'Description for Series 87', 'Author 87', 'Publisher 87', 'Ongoing', ARRAY['Action','Adventure'], DATE '2022-03-01', 4.3, 'https://placehold.co/200x300'),
('Series 88', 'Description for Series 88', 'Author 88', 'Publisher 88', 'Cancelled', ARRAY['Drama'], DATE '2022-04-01', 2.9, 'https://placehold.co/200x300'),
('Series 89', 'Description for Series 89', 'Author 89', 'Publisher 89', 'Ongoing', ARRAY['Fantasy'], DATE '2022-05-01', 4.6, 'https://placehold.co/200x300'),
('Series 90', 'Description for Series 90', 'Author 90', 'Publisher 90', 'Completed', ARRAY['Action','Comedy'], DATE '2022-06-01', 4.0, 'https://placehold.co/200x300'),
('Series 91', 'Description for Series 91', 'Author 91', 'Publisher 91', 'Ongoing', ARRAY['Action','Fantasy'], DATE '2022-07-01', 4.2, 'https://placehold.co/200x300'),
('Series 92', 'Description for Series 92', 'Author 92', 'Publisher 92', 'Completed', ARRAY['Adventure','Drama'], DATE '2022-08-01', 3.9, 'https://placehold.co/200x300'),
('Series 93', 'Description for Series 93', 'Author 93', 'Publisher 93', 'Ongoing', ARRAY['Comedy','Slice of Life'], DATE '2022-09-01', 4.5, 'https://placehold.co/200x300'),
('Series 94', 'Description for Series 94', 'Author 94', 'Publisher 94', 'Ongoing', ARRAY['Sci-Fi','Action'], DATE '2022-10-01', 4.1, 'https://placehold.co/200x300'),
('Series 95', 'Description for Series 95', 'Author 95', 'Publisher 95', 'Completed', ARRAY['Romance','Drama'], DATE '2022-11-01', 3.8, 'https://placehold.co/200x300'),
('Series 96', 'Description for Series 96', 'Author 96', 'Publisher 96', 'Ongoing', ARRAY['Horror','Mystery'], DATE '2022-12-01', 4.0, 'https://placehold.co/200x300'),
('Series 97', 'Description for Series 97', 'Author 97', 'Publisher 97', 'Ongoing', ARRAY['Action','Adventure'], DATE '2023-01-01', 4.3, 'https://placehold.co/200x300'),
('Series 98', 'Description for Series 98', 'Author 98', 'Publisher 98', 'Cancelled', ARRAY['Drama'], DATE '2023-02-01', 2.9, 'https://placehold.co/200x300'),
('Series 99', 'Description for Series 99', 'Author 99', 'Publisher 99', 'Ongoing', ARRAY['Fantasy'], DATE '2023-03-01', 4.6, 'https://placehold.co/200x300'),
('Series 100', 'Description for Series 100', 'Author 100', 'Publisher 100', 'Completed', ARRAY['Action','Comedy'], DATE '2023-04-01', 4.0, 'https://placehold.co/200x300');

-- Chapters: 3 per series
-- For deterministic ids, rely on serial assignment in insertion order
DO $$
DECLARE s RECORD;
BEGIN
  FOR s IN SELECT id FROM series LOOP
    INSERT INTO chapters(series_id, title, page_count, pages)
    VALUES
      (s.id, 'Chapter 1', 10, ARRAY[
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000'
      ]),
      (s.id, 'Chapter 2', 8, ARRAY[
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000'
      ]),
      (s.id, 'Chapter 3', 8, ARRAY[
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000', 'https://placehold.co/700x1000',
        'https://placehold.co/700x1000', 'https://placehold.co/700x1000'
      ]);
  END LOOP;
END $$;

-- Ratings: 3 users per series with varying stars
INSERT INTO ratings(series_id, user_id, stars)
SELECT s.id, u.user_id, ((s.id + u.user_id) % 5) + 1 AS stars
FROM series s
CROSS JOIN (VALUES (1),(2),(3)) AS u(user_id);


