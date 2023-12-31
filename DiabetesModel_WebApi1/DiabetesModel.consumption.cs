﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
public partial class DiabetesModel
{
    /// <summary>
    /// model input class for DiabetesModel.
    /// </summary>
    #region model input class
    public class ModelInput
    {
        [LoadColumn(0)]
        [ColumnName(@"Diabetes_012")]
        public float Diabetes_012 { get; set; }

        [LoadColumn(1)]
        [ColumnName(@"HighBP")]
        public float HighBP { get; set; }

        [LoadColumn(2)]
        [ColumnName(@"HighChol")]
        public float HighChol { get; set; }

        [LoadColumn(3)]
        [ColumnName(@"CholCheck")]
        public float CholCheck { get; set; }

        [LoadColumn(4)]
        [ColumnName(@"BMI")]
        public float BMI { get; set; }

        [LoadColumn(5)]
        [ColumnName(@"Smoker")]
        public float Smoker { get; set; }

        [LoadColumn(6)]
        [ColumnName(@"Stroke")]
        public float Stroke { get; set; }

        [LoadColumn(7)]
        [ColumnName(@"HeartDiseaseorAttack")]
        public float HeartDiseaseorAttack { get; set; }

        [LoadColumn(8)]
        [ColumnName(@"PhysActivity")]
        public float PhysActivity { get; set; }

        [LoadColumn(9)]
        [ColumnName(@"Fruits")]
        public float Fruits { get; set; }

        [LoadColumn(10)]
        [ColumnName(@"Veggies")]
        public float Veggies { get; set; }

        [LoadColumn(11)]
        [ColumnName(@"HvyAlcoholConsump")]
        public float HvyAlcoholConsump { get; set; }

        [LoadColumn(12)]
        [ColumnName(@"AnyHealthcare")]
        public float AnyHealthcare { get; set; }

        [LoadColumn(13)]
        [ColumnName(@"NoDocbcCost")]
        public float NoDocbcCost { get; set; }

        [LoadColumn(14)]
        [ColumnName(@"GenHlth")]
        public float GenHlth { get; set; }

        [LoadColumn(15)]
        [ColumnName(@"MentHlth")]
        public float MentHlth { get; set; }

        [LoadColumn(16)]
        [ColumnName(@"PhysHlth")]
        public float PhysHlth { get; set; }

        [LoadColumn(17)]
        [ColumnName(@"DiffWalk")]
        public float DiffWalk { get; set; }

        [LoadColumn(18)]
        [ColumnName(@"Sex")]
        public float Sex { get; set; }

        [LoadColumn(19)]
        [ColumnName(@"Age")]
        public float Age { get; set; }

        [LoadColumn(20)]
        [ColumnName(@"Education")]
        public float Education { get; set; }

        [LoadColumn(21)]
        [ColumnName(@"Income")]
        public float Income { get; set; }

    }

    #endregion

    /// <summary>
    /// model output class for DiabetesModel.
    /// </summary>
    #region model output class
    public class ModelOutput
    {
        [ColumnName(@"Diabetes_012")]
        public uint Diabetes_012 { get; set; }

        [ColumnName(@"HighBP")]
        public float HighBP { get; set; }

        [ColumnName(@"HighChol")]
        public float HighChol { get; set; }

        [ColumnName(@"CholCheck")]
        public float CholCheck { get; set; }

        [ColumnName(@"BMI")]
        public float BMI { get; set; }

        [ColumnName(@"Smoker")]
        public float Smoker { get; set; }

        [ColumnName(@"Stroke")]
        public float Stroke { get; set; }

        [ColumnName(@"HeartDiseaseorAttack")]
        public float HeartDiseaseorAttack { get; set; }

        [ColumnName(@"PhysActivity")]
        public float PhysActivity { get; set; }

        [ColumnName(@"Fruits")]
        public float Fruits { get; set; }

        [ColumnName(@"Veggies")]
        public float Veggies { get; set; }

        [ColumnName(@"HvyAlcoholConsump")]
        public float HvyAlcoholConsump { get; set; }

        [ColumnName(@"AnyHealthcare")]
        public float AnyHealthcare { get; set; }

        [ColumnName(@"NoDocbcCost")]
        public float NoDocbcCost { get; set; }

        [ColumnName(@"GenHlth")]
        public float GenHlth { get; set; }

        [ColumnName(@"MentHlth")]
        public float MentHlth { get; set; }

        [ColumnName(@"PhysHlth")]
        public float PhysHlth { get; set; }

        [ColumnName(@"DiffWalk")]
        public float DiffWalk { get; set; }

        [ColumnName(@"Sex")]
        public float Sex { get; set; }

        [ColumnName(@"Age")]
        public float Age { get; set; }

        [ColumnName(@"Education")]
        public float Education { get; set; }

        [ColumnName(@"Income")]
        public float Income { get; set; }

        [ColumnName(@"Features")]
        public float[] Features { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }

    }

    #endregion

    private static string MLNetModelPath = Path.GetFullPath("DiabetesModel.mlnet");

    public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


    private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
    {
        var mlContext = new MLContext();
        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
    }

    /// <summary>
    /// Use this method to predict scores for all possible labels.
    /// </summary>
    /// <param name="input">model input.</param>
    /// <returns><seealso cref=" ModelOutput"/></returns>
    public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAllLabels(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        var result = predEngine.Predict(input);
        return GetSortedScoresWithLabels(result);
    }

    /// <summary>
    /// Map the unlabeled result score array to the predicted label names.
    /// </summary>
    /// <param name="result">Prediction to get the labeled scores from.</param>
    /// <returns>Ordered list of label and score.</returns>
    /// <exception cref="Exception"></exception>
    public static IOrderedEnumerable<KeyValuePair<string, float>> GetSortedScoresWithLabels(ModelOutput result)
    {
        var unlabeledScores = result.Score;
        var labelNames = GetLabels(result);

        Dictionary<string, float> labledScores = new Dictionary<string, float>();
        for (int i = 0; i < labelNames.Count(); i++)
        {
            // Map the names to the predicted result score array
            var labelName = labelNames.ElementAt(i);
            labledScores.Add(labelName.ToString(), unlabeledScores[i]);
        }

        return labledScores.OrderByDescending(c => c.Value);
    }

    /// <summary>
    /// Get the ordered label names.
    /// </summary>
    /// <param name="result">Predicted result to get the labels from.</param>
    /// <returns>List of labels.</returns>
    /// <exception cref="Exception"></exception>
    private static IEnumerable<string> GetLabels(ModelOutput result)
    {
        var schema = PredictEngine.Value.OutputSchema;

        var labelColumn = schema.GetColumnOrNull("Diabetes_012");
        if (labelColumn == null)
        {
            throw new Exception("Diabetes_012 column not found. Make sure the name searched for matches the name in the schema.");
        }

        // Key values contains an ordered array of the possible labels. This allows us to map the results to the correct label value.
        var keyNames = new VBuffer<float>();
        labelColumn.Value.GetKeyValues(ref keyNames);
        return keyNames.DenseValues().Select(x => x.ToString());
    }

    /// <summary>
    /// Use this method to predict on <see cref="ModelInput"/>.
    /// </summary>
    /// <param name="input">model input.</param>
    /// <returns><seealso cref=" ModelOutput"/></returns>
    public static ModelOutput Predict(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        return predEngine.Predict(input);
    }
}
