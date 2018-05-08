#!/usr/bin/env python
import datetime
import random

import matplotlib.pyplot as plt
from skimage import data, io
import numpy
import cv2
from scipy import ndimage
from PIL import Image
from sklearn.neighbors import KNeighborsClassifier


def generate_horizontal_line(image, line_size_x):
    return generate_line(image, 0, image.height / 2 - line_size_x / 2, image.width, image.height / 2 + line_size_x / 2)


def generate_vertical_line(image, line_size_y):
    return generate_line(image, image.width / 2 - line_size_y / 2, 0, image.width / 2 + line_size_y / 2, image.height)


def generate_line(image, start_x, start_y, end_x, end_y):
    crop_mask = (start_x, start_y, end_x, end_y)
    crop_img = image.crop(crop_mask)
    return crop_img


def generate_fragment(image, x, y, k):
    return generate_line(image, x, y, x + k, y + k)


def generate_fragment_using_middle_point(image, x, y, k):
    return generate_fragment(image, x - k / 2, y - k / 2, k)


def convert_image_to_array(img):
    return numpy.asarray(img.convert("L"))


def calculate_hu_moments(tab):
    return cv2.HuMoments(cv2.moments(tab)).flatten()


def get_middle_pixel_color(imge, size):
    return imge.load()[size / 2, size / 2]


def get_variation_color(img):
    return numpy.var(convert_image_to_array(img))


def generate_parameters_for_knn(fragment, size):
    image_array = convert_image_to_array(fragment)
    moments = calculate_hu_moments(image_array)
    middle_pixel_color = get_middle_pixel_color(fragment, size)
    variation = get_variation_color(fragment)
    brightness = 0  # TODO wyliczenie jasnosci obrazu
    edges = 0  # TODO wykrycie krawedzi przy uzyciu canny z cv
    variation_of_vertical_line = get_variation_color(generate_vertical_line(fragment, 4))
    variation_of_horizontal_line = get_variation_color(generate_horizontal_line(fragment, 4))

    params = [
        {"id": 1, "name": "moments", "value": moments},
        {"id": 2, "name": "middle_pixel_color", "value": middle_pixel_color},
        {"id": 3, "name": "variation", "value": variation},
        {"id": 4, "name": "brightness", "value": brightness},
        {"id": 5, "name": "edges", "value": edges},
        {"id": 6, "name": "variation_of_vertical_line", "value": variation_of_vertical_line},
        {"id": 7, "name": "variation_of_horizontal_line", "value": variation_of_horizontal_line}
    ]

    return generate_attributes_vector(params, 7)


def add_value_to_array(array, value):
    return array + [value]


def generate_attributes_vector(params, params_amount):
    array = []
    for i in range(params_amount):
        if hasattr(params[i]["value"], "__len__"):
            for value in params[i]["value"]:
                array = add_value_to_array(array, value)
        else:
            array = add_value_to_array(array, params[i]["value"])

    return array


def generate():
    im = Image.open('healthy/01_h.jpg')
    pix = im.load()
    h, w = im.size

    for y in range(1, h - 1):
        for x in range(1, w - 1):
            value_max = max(pix[y - 1, x - 1][0], pix[y - 1, x][0], pix[y - 1, x + 1][0], pix[y, x - 1][0],
                            pix[y, x][0], pix[y, x + 1][0], pix[y + 1, x - 1][0], pix[y + 1, x][0],
                            pix[y + 1, x + 1][0])
            if value_max == pix[y, x][0]:
                pix[y, x] = (255, 255, 255)
    im.save("01_h.png")


def fun():
    plt.figure(figsize=(16, 16))
    image = ['healthy/01_h.jpg']
    for i in image:
        plt.subplot(3, 1, 1)
        plt.axis('off')
        io.imshow(data.imread(i, as_grey=False))

        plt.subplot(3, 1, 2)
        plt.axis('off')
        grey = data.imread(i, as_grey=True)
        dx = ndimage.sobel(grey, 1)
        dy = ndimage.sobel(grey, 0)
        mag = numpy.hypot(dx, dy)
        mag *= 255.0 / numpy.max(mag)
        kernel = numpy.ones((5, 5), numpy.uint8)
        erosion = cv2.erode(mag, kernel, iterations=1)
        dilation = cv2.dilate(erosion, kernel, iterations=1)
        io.imshow(dilation)

        plt.subplot(3, 1, 3)
        plt.axis('off')
        io.imshow(data.imread(i, as_grey=True))

        plt.show()


def create_path_for_input_image(number):
    return 'healthy_mini/0' + str(number) + '_h.jpg' if number < 10 else 'healthy_mini/' + str(number) + '_h.jpg'


def generate_input_images_paths(start_from, end_on):
    images_paths = []
    for i in range(start_from, end_on):
        images_paths = images_paths + [create_path_for_input_image(i)]
    return images_paths


def generate_images_array(images_paths):
    images = []
    for image_path in images_paths:
        images = images + [Image.open(image_path)]
    return images


def generate_path_for_correct_answer_image(number):
    return 'healthy_correct_mini/0' + str(number) + '_h.tif' if number < 10 else 'healthy_correct_mini/' + str(number) + '_h.tif'


def generate_correct_answers_paths(start_from, end_on):
    images_paths = []
    for i in range(start_from, end_on):
        images_paths = images_paths + [generate_path_for_correct_answer_image(i)]
    return images_paths


def filter_image(image):
    # TODO filtrowanie obrazu jak w fun
    return image


def filter_images(images):
    return [filter_image(image) for image in images]


def choose_random_image(images):
    return random.choice(images)


def choose_random_pixel_coordinates(image, size_of_fragment):
    return random.randint(size_of_fragment, image.width - size_of_fragment), \
           random.randint(size_of_fragment, image.height - size_of_fragment)


def normalize_vector_of_parameters(vector_to_learn, max_values, min_values):
    for i in range(len(vector_to_learn)):
        if max_values[i] > 0:
            vector_to_learn[i] = (vector_to_learn[i] - min_values[i]) / (max_values[i] - min_values[i])
        else:
            vector_to_learn[i] = 0
    return vector_to_learn


def normalize_parameters_to_learn(parameters_to_learn, max_values, min_values):
    return [normalize_vector_of_parameters(vector, max_values, min_values) for vector in parameters_to_learn]


def generate_array_of_tuples_of_images(input_images, correct_answers_images):
    array_of_tuples = []
    for i in range(len(input_images)):
        array_of_tuples.append((input_images[i], correct_answers_images[i],))
    return array_of_tuples


def get_correct_answer_for_pixel(image, x, y):
    return 1 if image.getpixel((x, y)) > 128 else 0


def get_image_to_predict(image, size_of_fragment):
    return generate_line(image, size_of_fragment, size_of_fragment,
                         image.width - size_of_fragment, image.height - size_of_fragment)


def get_images_to_predict(images, size_of_fragment):
    return [get_image_to_predict(image, size_of_fragment) for image in images]


def predict_pixel(image, x, y, size_of_fragment, max_values, min_values, knn):
    fragment = generate_fragment_using_middle_point(image, x, y, size_of_fragment)
    parameters = generate_parameters_for_knn(fragment, size_of_fragment)
    normalized_parameters = normalize_vector_of_parameters(parameters, max_values, min_values)

    return knn.predict([normalized_parameters])


def predict_image(knn, image, size_of_fragment, max_values, min_values):
    return [[predict_pixel(image, x, y, size_of_fragment, max_values, min_values, knn)
             for y in range(image.height)] for x in range(image.width)]


def generate_new_max_values(old_max, vector):
    if len(old_max) == 0:
        return vector
    else:
        new_max = numpy.zeros(len(old_max))
        for i in range(len(vector)):
            new_max[i] = vector[i] if vector[i] >= old_max[i] else old_max[i]

        return new_max


def generate_new_min_values(old_min, vector):
    if len(old_min) == 0:
        return vector
    else:
        new_min = numpy.zeros(len(old_min))
        for i in range(len(vector)):
            new_min[i] = vector[i] if vector[i] <= old_min[i] else old_min[i]

        return new_min


def generate_image_from_array(array):
    w, h = len(array), len(array[0])
    array_for_image = numpy.zeros((w, h, 3), dtype=numpy.uint8)

    for x in range(w):
        for y in range(h):
            array_for_image[y][x] = [255, 255, 0] if array[x][y] == 1 else [0, 0, 0]

    img = Image.fromarray(array_for_image, 'RGB')
    img.show()


def main():
    seed = 10
    amount_of_learning_point = 5000
    size_of_fragment = 11
    start_from = 1
    end_on = 16
    amount_of_neighbors = 6

    random.seed(seed)

    input_images_paths = generate_input_images_paths(start_from, end_on)
    correct_answers_images_paths = generate_correct_answers_paths(start_from, end_on)

    input_images = generate_images_array(input_images_paths)
    correct_answers_images = generate_images_array(correct_answers_images_paths)
    filtered_images = filter_images(input_images)

    tuples_array = generate_array_of_tuples_of_images(filtered_images, correct_answers_images)

    parameters_to_learn = []
    answers_to_learn = []
    max_values = []
    min_values = []

    print(datetime.datetime.now())

    for i in range(amount_of_learning_point):
        chosen_pictures = choose_random_image(tuples_array)

        input_image = chosen_pictures[0]
        correct_answer_image = chosen_pictures[1]

        x, y = choose_random_pixel_coordinates(input_image, size_of_fragment)
        fragment = generate_fragment_using_middle_point(input_image, x, y, size_of_fragment)

        parameters = generate_parameters_for_knn(fragment, size_of_fragment)
        parameters_to_learn = parameters_to_learn + [parameters]

        correct_answer = get_correct_answer_for_pixel(correct_answer_image, x, y)
        answers_to_learn.append(correct_answer)

        max_values = generate_new_max_values(max_values, parameters)
        min_values = generate_new_min_values(min_values, parameters)

    normalized_parameters_to_learn = normalize_parameters_to_learn(parameters_to_learn, max_values, min_values)

    print(datetime.datetime.now())

    knn = KNeighborsClassifier(n_neighbors=amount_of_neighbors)
    knn.fit(normalized_parameters_to_learn, answers_to_learn)

    print(datetime.datetime.now())

    # predict = knn.predict([normalized_parameters_to_learn[0]])
    # print(predict)

    predictions = predict_image(knn, get_image_to_predict(tuples_array[0][0], size_of_fragment),
                                size_of_fragment, max_values, min_values)

    print(datetime.datetime.now())

    generate_image_from_array(predictions)

   # print(predictions)


main()
