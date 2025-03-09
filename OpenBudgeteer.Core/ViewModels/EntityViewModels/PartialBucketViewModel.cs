using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using OpenBudgeteer.Core.Common.EventClasses;
using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.Data.Entities.Models;

namespace OpenBudgeteer.Core.ViewModels.EntityViewModels;

public class PartialBucketViewModel : ViewModelBase, ICloneable, IEquatable<PartialBucketViewModel>, IComparable<PartialBucketViewModel>
{
    #region Properties & Fields
    
    private Guid _selectedBucketId;
    /// <summary>
    /// Database Id of the selected Bucket
    /// </summary>
    public Guid SelectedBucketId 
    { 
        get => _selectedBucketId;
        set => Set(ref _selectedBucketId, value);
    }
    
    private string _selectedBucketName;
    /// <summary>
    /// Name of the selected Bucket
    /// </summary>
    public string SelectedBucketName
    {
        get => _selectedBucketName;
        set => Set(ref _selectedBucketName, value);
    }
    
    private string _selectedBucketColorCode;
    /// <summary>
    /// Name of the background color based from <see cref="Color"/>
    /// </summary>
    public string SelectedBucketColorCode 
    { 
        get => _selectedBucketColorCode;
        set => Set(ref _selectedBucketColorCode, value);
    }
    
    /// <summary>
    /// Background <see cref="Color"/> of the selected Bucket 
    /// </summary>
    public Color SelectedBucketColor => string.IsNullOrEmpty(SelectedBucketColorCode) ? Color.LightGray : Color.FromName(SelectedBucketColorCode);
    
    private string _selectedBucketTextColorCode;
    /// <summary>
    /// Name of the text color based from <see cref="Color"/>
    /// </summary>
    public string SelectedBucketTextColorCode 
    { 
        get => _selectedBucketTextColorCode;
        set => Set(ref _selectedBucketTextColorCode, value);
    }
    
    /// <summary>
    /// Text <see cref="Color"/> of the selected Bucket 
    /// </summary>
    public Color SelectedBucketTextColor => string.IsNullOrEmpty(SelectedBucketTextColorCode) ? Color.Black : Color.FromName(SelectedBucketTextColorCode);

    private decimal _amount;
    /// <summary>
    /// Money that will be assigned to this Bucket
    /// </summary>
    public decimal Amount
    {
        get => _amount;
        set
        {
            var oldValue = _amount;
            Set(ref _amount, value);
            if (_amount != oldValue) AmountChanged?.Invoke(this, new AmountChangedArgs(this, value));
        }
    }

    /// <summary>
    /// EventHandler which should be invoked once amount assigned to this Bucket has been changed. Can be used
    /// to start further consistency checks and other calculations based on this change 
    /// </summary>
    public event EventHandler<AmountChangedArgs>? AmountChanged;
    
    /// <summary>
    /// EventHandler which should be invoked in case this instance should start its deletion process. Can be used
    /// in case the way how this instance will be deleted is handled outside of this class
    /// </summary>
    public event EventHandler<DeleteAssignmentRequestArgs>? DeleteAssignmentRequest;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Initialize ViewModel based on a existing <see cref="Bucket"/> object
    /// </summary>
    /// <param name="serviceManager">Reference to API based services</param>
    /// <param name="bucket">Bucket instance</param>
    /// <param name="amount">Amount to be assigned to this Bucket</param>
    protected PartialBucketViewModel(IServiceManager serviceManager, Bucket? bucket, decimal amount) : base(serviceManager)
    {
        _amount = amount;

        if (bucket is null)
        {
            // Create a "No Selection" Bucket
            var noSelectionBucket = new Bucket()
            {
                Id = Guid.Empty,
                BucketGroupId = Guid.Empty,
                BucketGroup = new BucketGroup(),
                Name = "No Selection"
            };
            
            _selectedBucketId = noSelectionBucket.Id;
            _selectedBucketName = noSelectionBucket.Name ?? string.Empty;
            _selectedBucketColorCode = noSelectionBucket.ColorCode ?? string.Empty;
            _selectedBucketTextColorCode = noSelectionBucket.TextColorCode ?? string.Empty;
        }
        else
        {
            _selectedBucketId = bucket.Id;
            _selectedBucketName = bucket.Name ?? string.Empty;
            _selectedBucketColorCode = bucket.ColorCode ?? string.Empty;
            _selectedBucketTextColorCode = bucket.TextColorCode ?? string.Empty;
        }
    }

    /// <summary>
    /// Initialize a copy of the passed ViewModel
    /// </summary>
    /// <param name="viewModel">Current ViewModel instance</param>
    public PartialBucketViewModel(PartialBucketViewModel viewModel) : base(viewModel.ServiceManager)
    {
        SelectedBucketId = viewModel.SelectedBucketId;
        _selectedBucketName = viewModel.SelectedBucketName;
        _selectedBucketColorCode = viewModel.SelectedBucketColorCode;
        _selectedBucketTextColorCode = viewModel.SelectedBucketTextColorCode;
        _amount = viewModel.Amount;
    }

    /// <summary>
    /// Initialize ViewModel with a "No Selection" <see cref="Bucket"/>
    /// </summary>
    /// <param name="serviceManager">Reference to API based services</param>
    /// <param name="amount">Amount to be assigned to this Bucket</param>
    public static PartialBucketViewModel CreateNoSelection(IServiceManager serviceManager, decimal amount = 0)
    {
        return new PartialBucketViewModel(serviceManager, null, amount);
    }

    /// <summary>
    /// Initialize ViewModel based on an existing <see cref="Bucket"/> object and the final amount to be assigned
    /// </summary>
    /// <param name="serviceManager">Reference to API based services</param>
    /// <param name="bucket">Bucket instance</param>
    /// <param name="amount">Amount to be assigned to this Bucket</param>
    public static PartialBucketViewModel CreateFromBucket(IServiceManager serviceManager, Bucket bucket, decimal amount)
    {
        return new PartialBucketViewModel(serviceManager, bucket, amount);
    }

    /// <summary>
    /// Return a deep copy of the ViewModel
    /// </summary>
    public object Clone()
    {
        return new PartialBucketViewModel(this);
    }

    
    #endregion
    
    #region Modification Handler

    /// <summary>
    /// Triggers <see cref="DeleteAssignmentRequest"/>
    /// </summary>
    public void DeleteBucket()
    {
        DeleteAssignmentRequest?.Invoke(this, new DeleteAssignmentRequestArgs(this));
    }

    /// <summary>
    /// Updates ViewModels Bucket data based on passed <see cref="BucketViewModel"/> object
    /// </summary>
    /// <param name="bucketViewModel">Newly selected Bucket</param>
    public void UpdateSelectedBucket(BucketViewModel bucketViewModel)
    {
        SelectedBucketId = bucketViewModel.BucketId;
        SelectedBucketName = bucketViewModel.Name;
        SelectedBucketColorCode = bucketViewModel.ColorCode;
        SelectedBucketTextColorCode = bucketViewModel.TextColorCode;
    }
    
    #endregion

    #region IEquatable & IComparable Implementation

    public bool Equals(PartialBucketViewModel? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return
            _selectedBucketId.Equals(other._selectedBucketId) &&
            _selectedBucketName == other._selectedBucketName &&
            _selectedBucketColorCode == other._selectedBucketColorCode &&
            _selectedBucketTextColorCode == other._selectedBucketTextColorCode;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PartialBucketViewModel)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(_selectedBucketId);
        hashCode.Add(_selectedBucketName);
        hashCode.Add(_selectedBucketColorCode);
        hashCode.Add(_selectedBucketTextColorCode);
        return hashCode.ToHashCode();
    }
    
    public int CompareTo(PartialBucketViewModel? other)
    {
        return string.Compare(_selectedBucketName, other?.SelectedBucketName, StringComparison.Ordinal);
    }

    public override string ToString() => SelectedBucketName;

    #endregion
}