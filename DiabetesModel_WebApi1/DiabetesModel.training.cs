﻿// This file was auto-generated by ML.NET Model Builder.
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML;
    public partial class DiabetesModel
    {
        public const string RetrainFilePath =  @"D:\Pie\Dev\Hospify_ML\Hospify_ML\Dataset\Diabetes Health Indicators Dataset\diabetes_012_health_indicators_BRFSS2015.csv";
        public const char RetrainSeparatorChar = ',';
        public const bool RetrainHasHeader =  true;

         /// <summary>
        /// Train a new model with the provided dataset.
        /// </summary>
        /// <param name="outputModelPath">File path for saving the model. Should be similar to "C:\YourPath\ModelName.mlnet"</param>
        /// <param name="inputDataFilePath">Path to the data file for training.</param>
        /// <param name="separatorChar">Separator character for delimited training file.</param>
        /// <param name="hasHeader">Boolean if training file has a header.</param>
        public static void Train(string outputModelPath, string inputDataFilePath = RetrainFilePath, char separatorChar = RetrainSeparatorChar, bool hasHeader = RetrainHasHeader)
        {
            var mlContext = new MLContext();

            var data = LoadIDataViewFromFile(mlContext, inputDataFilePath, separatorChar, hasHeader);
            var model = RetrainModel(mlContext, data);
            SaveModel(mlContext, model, data, outputModelPath);
        }

        /// <summary>
        /// Load an IDataView from a file path.
        /// </summary>
        /// <param name="mlContext">The common context for all ML.NET operations.</param>
        /// <param name="inputDataFilePath">Path to the data file for training.</param>
        /// <param name="separatorChar">Separator character for delimited training file.</param>
        /// <param name="hasHeader">Boolean if training file has a header.</param>
        /// <returns>IDataView with loaded training data.</returns>
        public static IDataView LoadIDataViewFromFile(MLContext mlContext, string inputDataFilePath, char separatorChar, bool hasHeader)
        {
            return mlContext.Data.LoadFromTextFile<ModelInput>(inputDataFilePath, separatorChar, hasHeader);
        }


        /// <summary>
        /// Save a model at the specified path.
        /// </summary>
        /// <param name="mlContext">The common context for all ML.NET operations.</param>
        /// <param name="model">Model to save.</param>
        /// <param name="data">IDataView used to train the model.</param>
        /// <param name="modelSavePath">File path for saving the model. Should be similar to "C:\YourPath\ModelName.mlnet.</param>
        public static void SaveModel(MLContext mlContext, ITransformer model, IDataView data, string modelSavePath)
        {
            // Pull the data schema from the IDataView used for training the model
            DataViewSchema dataViewSchema = data.Schema;

            using (var fs = File.Create(modelSavePath))
            {
                mlContext.Model.Save(model, dataViewSchema, fs);
            }
        }


        /// <summary>
        /// Retrain model using the pipeline generated as part of the training process.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainModel(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"HighBP", @"HighBP"),new InputOutputColumnPair(@"HighChol", @"HighChol"),new InputOutputColumnPair(@"CholCheck", @"CholCheck"),new InputOutputColumnPair(@"BMI", @"BMI"),new InputOutputColumnPair(@"Smoker", @"Smoker"),new InputOutputColumnPair(@"Stroke", @"Stroke"),new InputOutputColumnPair(@"HeartDiseaseorAttack", @"HeartDiseaseorAttack"),new InputOutputColumnPair(@"PhysActivity", @"PhysActivity"),new InputOutputColumnPair(@"Fruits", @"Fruits"),new InputOutputColumnPair(@"Veggies", @"Veggies"),new InputOutputColumnPair(@"HvyAlcoholConsump", @"HvyAlcoholConsump"),new InputOutputColumnPair(@"AnyHealthcare", @"AnyHealthcare"),new InputOutputColumnPair(@"NoDocbcCost", @"NoDocbcCost"),new InputOutputColumnPair(@"GenHlth", @"GenHlth"),new InputOutputColumnPair(@"MentHlth", @"MentHlth"),new InputOutputColumnPair(@"PhysHlth", @"PhysHlth"),new InputOutputColumnPair(@"DiffWalk", @"DiffWalk"),new InputOutputColumnPair(@"Sex", @"Sex"),new InputOutputColumnPair(@"Age", @"Age"),new InputOutputColumnPair(@"Education", @"Education"),new InputOutputColumnPair(@"Income", @"Income")})      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"HighBP",@"HighChol",@"CholCheck",@"BMI",@"Smoker",@"Stroke",@"HeartDiseaseorAttack",@"PhysActivity",@"Fruits",@"Veggies",@"HvyAlcoholConsump",@"AnyHealthcare",@"NoDocbcCost",@"GenHlth",@"MentHlth",@"PhysHlth",@"DiffWalk",@"Sex",@"Age",@"Education",@"Income"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"Diabetes_012",inputColumnName:@"Diabetes_012",addKeyValueAnnotationsAsText:false))      
                                    .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(new LbfgsMaximumEntropyMulticlassTrainer.Options(){L1Regularization=3.832216F,L2Regularization=1.407778F,LabelColumnName=@"Diabetes_012",FeatureColumnName=@"Features"}))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName:@"PredictedLabel",inputColumnName:@"PredictedLabel"));

            return pipeline;
        }
    }
 