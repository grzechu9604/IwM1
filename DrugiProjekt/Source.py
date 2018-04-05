#!/usr/bin/env python
import matplotlib
import matplotlib.pyplot as plt
from skimage import data, io, feature
import numpy
import scipy
import cv2
from scipy import ndimage

def version_1():
	plt.figure(figsize=(16,16))
	image = ['healthy/01_h.jpg']
	obrazy = []
	for i in image:
		plt.subplot(1, 2, 1)
		plt.axis('off')
		io.imshow(data.imread(i, as_grey=False))
		
		plt.subplot(1, 2, 2)
		plt.axis('off')
		grey = data.imread(i, as_grey=True)
		dx = ndimage.sobel(grey, 1)
		dy = ndimage.sobel(grey, 0)
		mag = numpy.hypot(dx, dy)  
		mag *= 255.0 / numpy.max(mag)
		kernel = numpy.ones((5,5),numpy.uint8)
		erosion = cv2.erode(mag,kernel,iterations = 1)	
		ret,thresh1 = cv2.threshold(erosion,10,255,cv2.THRESH_BINARY)
		dilation = cv2.dilate(thresh1,kernel,iterations = 1)
		afterFourier = dilation.astype(numpy.uint8)
		median = cv2.medianBlur(afterFourier,5)
		io.imshow(median)
		plt.show()

def version_2():
	plt.figure(figsize=(16,16))
	image = ['healthy/01_h.jpg']
	obrazy = []
	for i in image:
		plt.subplot(1, 2, 1)
		plt.axis('off')
		io.imshow(data.imread(i, as_grey=False))
		
		plt.subplot(1, 2, 2)
		plt.axis('off')
		grey = data.imread(i, as_grey=True)
		dx = ndimage.sobel(grey, 1)
		dy = ndimage.sobel(grey, 0)
		mag = numpy.hypot(dx, dy)  
		mag *= 255.0 / numpy.max(mag)
		kernel = numpy.ones((5,5),numpy.uint8)
		erosion = cv2.erode(mag,kernel,iterations = 1)	
		dilation = cv2.dilate(erosion,kernel,iterations = 1)
		io.imshow(dilation)
		plt.show()

def main():
	version_1()
	version_2()

main()