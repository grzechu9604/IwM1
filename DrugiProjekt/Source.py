#!/usr/bin/env python
import datetime
import random
import scipy

import matplotlib.pyplot as plt
from skimage import data, io
import numpy
import cv2
from scipy import ndimage
from PIL import Image, ImageStat
from sklearn.neighbors import KNeighborsClassifier
from skimage import feature

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


def get_fragment_brightness(img):
    stat = ImageStat.Stat(img)
    return stat.mean[0]


def get_image_edges(image):
    # TODO wyszukiwanie krawędzi
    return 0


def generate_parameters_for_knn(fragment, size):
    image_array = convert_image_to_array(fragment)
    moments = calculate_hu_moments(image_array)
    middle_pixel_color = get_middle_pixel_color(fragment, size)
    variation = get_variation_color(fragment)
    brightness = get_fragment_brightness(fragment)
    edges = get_image_edges(fragment)
    variation_of_vertical_line = get_variation_color(generate_vertical_line(fragment, 5))
    variation_of_horizontal_line = get_variation_color(generate_horizontal_line(fragment, 5))

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
    image_correct = ['healthy_correct_mini/01_h.tif']
    obrazy = []
    for i in image:
        plt.subplot(3, 1, 1)
        plt.axis('off')
        io.imshow(data.imread(i, as_grey=False))

        plt.subplot(3, 1, 2)
        plt.axis('off')
        inputImage = cv2.imread(i)
        dst = cv2.fastNlMeansDenoisingColored(inputImage, None, 10, 10, 7, 21)
        next = scipy.ndimage.filters.median_filter(dst, 10)
        gray = cv2.cvtColor(next, cv2.COLOR_BGR2GRAY)
        gray = numpy.float64(gray)
        dx = ndimage.sobel(gray, 0)
        dy = ndimage.sobel(gray, 1)
        mag = numpy.hypot(dx, dy)
        mag *= 255.0 / numpy.max(mag)
        kernel = numpy.ones((5, 5), numpy.uint8)
        erosion = cv2.erode(mag, kernel, iterations=1)
        dilation = cv2.dilate(erosion, kernel, iterations=1)
        w = inputImage.shape[1]
        h = inputImage.shape[0]
        for k in range(0, h):
            for t in range(0, w):
                if(dilation[k,t]>5):
                    dilation[k,t] = 255
                else:
                    dilation[k,t] = 0
        io.imshow(dilation)

        plt.subplot(3, 1, 3)
        plt.axis('off')

        picture2 = cv2.fastNlMeansDenoisingColored(inputImage, None, 10, 10, 7, 21)
        clone = inputImage.copy()
        lowerred = numpy.array([5, 5, 5])
        upperred = numpy.array([255, 255, 255])
        mask = cv2.inRange(picture2, lowerred, upperred)
        k = 12
        gray = cv2.cvtColor(cv2.addWeighted(clone, k / 10, numpy.zeros(picture2.shape, picture2.dtype), 0, 100),
                            cv2.COLOR_BGR2GRAY)
        graythreshed2 = cv2.adaptiveThreshold(gray, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 189 - k * 4,
                                              2 + k / 5)
        graythreshed2 = cv2.bitwise_and(graythreshed2, graythreshed2, mask=mask)
        mask2 = cv2.bitwise_not(mask)
        graythreshed2 = cv2.bitwise_or(graythreshed2, mask2)
        bilateralfilted = cv2.bilateralFilter(graythreshed2, 5, 175, 175)
        edge = cv2.Canny(bilateralfilted, 100, 1200)

        _, contours, _ = cv2.findContours(edge, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
        contour_list = []
        for contour in contours:
            contour_list.append(contour)

        clone2 = numpy.zeros((h, w))

        for j in range(len(contour_list)):
            M = cv2.moments(contour_list[j])
            cv2.drawContours(clone2, [contour_list[j]], -1, (255, 255, 255), 3)
        io.imshow(clone2)
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
    return 'healthy_correct_mini/0' + str(number) + '_h.tif' if number < 10 else 'healthy_correct_mini/' \
                                                                                 + str(number) + '_h.tif'


def generate_correct_answers_paths(start_from, end_on):
    images_paths = []
    for i in range(start_from, end_on):
        images_paths = images_paths + [generate_path_for_correct_answer_image(i)]
    return images_paths


def filter_image(image):
    # TODO filtrowanie
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


def get_normalized_parameters_for_pixel(image, x, y, size_of_fragment, max_values, min_values):
    fragment = generate_fragment_using_middle_point(image, x, y, size_of_fragment)
    parameters = generate_parameters_for_knn(fragment, size_of_fragment)
    normalized_parameters = normalize_vector_of_parameters(parameters, max_values, min_values)

    return normalized_parameters


def get_normalized_parameters_for_image(image, size_of_fragment, max_values, min_values):
    return [[get_normalized_parameters_for_pixel(image, x, y, size_of_fragment, max_values, min_values)
             for y in range(image.height)] for x in range(image.width)]


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
    h, w = len(array), len(array[0])
    array_for_image = numpy.zeros((w, h, 3), dtype=numpy.uint8)

    for x in range(w):
        for y in range(h):
            array_for_image[x][y] = [255, 255, 255] if array[y][x] == 1 else [0, 0, 0]

    img = Image.fromarray(array_for_image, 'RGB')
    img.show()
    img.save("Wynik.jpg")
    return img


def generate_misses_table(original_image, test_image, size_of_fragment):
    true_true = 0
    true_false = 0
    false_true = 0
    false_false = 0

    original_table = convert_image_to_array(original_image)
    test_table = test_image

    for x in range(len(test_table[0])):
        for y in range(len(test_table)):
            if test_table[x][y] > 0 and original_table[x + size_of_fragment][y + size_of_fragment] > 0:
                true_true += 1
            if test_table[x][y] > 0 and original_table[x + size_of_fragment][y + size_of_fragment] == 0:
                true_false += 1
            if test_table[x][y] == 0 and original_table[x + size_of_fragment][y + size_of_fragment] > 0:
                false_true += 1
            if test_table[x][y] == 0 and original_table[x + size_of_fragment][y + size_of_fragment] == 0:
                false_false += 1

    print(true_true, true_false, false_true, false_false)
    return [true_true, true_false, false_true, false_false]


def generate_misses_table_for_array(original_image, test, size_of_fragment):
    true_true = 0
    true_false = 0
    false_true = 0
    false_false = 0

    original_table = convert_image_to_array(original_image)
    test_table = test

    for x in range(len(test_table[0])):
        for y in range(len(test_table)):
            if test_table[x][y] > 0 and original_table[x + size_of_fragment][y + size_of_fragment] > 0:
                true_true += 1
            if test_table[x][y] > 0 and original_table[x + size_of_fragment][y + size_of_fragment] == 0:
                true_false += 1
            if test_table[x][y] == 0 and original_table[x + size_of_fragment][y + size_of_fragment] > 0:
                false_true += 1
            if test_table[x][y] == 0 and original_table[x + size_of_fragment][y + size_of_fragment] == 0:
                false_false += 1

    print(true_true, true_false, false_true, false_false)
    return [true_true, true_false, false_true, false_false]


def do_experiment(amount_of_learning_point, size_of_fragment, amount_of_neighbors):
    seed = 10
    start_from = 1
    end_on = 11

    start_test_from = 11
    end_test_on = 15

    max_amount_of_learning_point = 50000

    random.seed(seed)

    input_images_paths = generate_input_images_paths(start_from, end_on)
    correct_answers_images_paths = generate_correct_answers_paths(start_from, end_on)

    test_input_images_paths = generate_input_images_paths(start_test_from, end_test_on)
    test_correct_answers_images_paths = generate_correct_answers_paths(start_test_from, end_test_on)

    input_images = generate_images_array(input_images_paths)
    correct_answers_images = generate_images_array(correct_answers_images_paths)
    filtered_images = filter_images(input_images)

    test_input_images = generate_images_array(test_input_images_paths)
    test_correct_answers_images = generate_images_array(test_correct_answers_images_paths)
    test_filtered_images = filter_images(test_input_images)

    tuples_array = generate_array_of_tuples_of_images(filtered_images, correct_answers_images)
    test_tuples_array = generate_array_of_tuples_of_images(test_filtered_images, test_correct_answers_images)

    parameters_to_learn = []
    answers_to_learn = []
    max_values = []
    min_values = []

    for i in range(max_amount_of_learning_point):
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

    global_misses_table = [0, 0, 0, 0]

    for test_tuple_image in [test_tuples_array[0]]:
        normalized_parameters = get_normalized_parameters_for_image(test_tuple_image[0], size_of_fragment,
                                                                    max_values, min_values)
        for amount_of_neighbors in range(5, 100, 5):
                for amount_of_learning_point in range(30000, 50000, 1000):
                        knn = KNeighborsClassifier(n_neighbors=amount_of_neighbors)
                        knn.fit(normalized_parameters_to_learn[:amount_of_learning_point], answers_to_learn[:amount_of_learning_point])

                        predictions = []

                        predicted_array = [knn.predict(parameters) for parameters in normalized_parameters]

                        generate_image_from_array(predicted_array)

                        misses_table = generate_misses_table(test_tuple_image[1], predicted_array, size_of_fragment)

                        global_misses_table[0] += misses_table[0]
                        global_misses_table[1] += misses_table[1]
                        global_misses_table[2] += misses_table[2]
                        global_misses_table[3] += misses_table[3]

                        print(amount_of_learning_point, amount_of_neighbors, size_of_fragment, global_misses_table[0])


def main2():
    best = [0, 0, 0, 0]
    for amount_of_learning_point in range(19000, 20000, 1000):
        for amount_of_neighbors in range(5, 100, 5):
            for size_of_fragment in range(9, 21, 2):
                result = do_experiment(amount_of_learning_point, size_of_fragment, amount_of_neighbors)
                if result[3] > best[3]:
                    best = result

    print("Best!")
    print(best)

def main():
    do_experiment(16000, 13, 5)


fun()
#main()
